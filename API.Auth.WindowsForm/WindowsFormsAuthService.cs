using DTOs.Usuarios;
using DTOs.Auth;
using API.Clients;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace API.Auth.WindowsForm
{
    public class WindowsFormsAuthService : IAuthService
    {
        private static string? _currentToken;
        private static DateTime _tokenExpiration;
        private static string? _currentUsername;
        private static UsuarioDTO? _currentUser;

        public event Action<bool>? AuthenticationStateChanged;

        public Task<bool> IsAuthenticatedAsync()
        {
            return Task.FromResult(!string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration);
        }

        public Task<string?> GetTokenAsync()
        {
            var isAuth = !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
            return Task.FromResult(isAuth ? _currentToken : null);
        }
        
        public Task<string?> GetUsernameAsync()
        {
            var isAuth = !string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow < _tokenExpiration;
            return Task.FromResult(isAuth ? _currentUsername : null);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // Obtener la URL base desde configuración
                string baseUrl = GetBaseUrlFromConfig();
                
                // Crear HttpClient configurado manualmente para login
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl),
                    Timeout = TimeSpan.FromSeconds(30)
                };
                
                // Llamar al endpoint de login que devuelve JWT
                var loginRequest = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var json = JsonSerializer.Serialize(loginRequest);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var httpResponse = await httpClient.PostAsync("auth/login", content);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonSerializer.Deserialize<LoginResponse>(responseContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    if (response != null)
                    {
                        // Guardar el token JWT real
                        _currentToken = response.Token;
                        _tokenExpiration = response.ExpiresAt;
                        _currentUsername = response.Username;
                        
                        // Extraer información del token JWT
                        var handler = new JwtSecurityTokenHandler();
                        var jsonToken = handler.ReadJwtToken(response.Token);
                        
                        // Opcionalmente, crear un UsuarioDTO desde los claims
                        var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                        var emailClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                        
                        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                        {
                            _currentUser = new UsuarioDTO
                            {
                                Id = userId,
                                Username = response.Username,
                                Email = emailClaim?.Value ?? string.Empty
                            };
                        }

                        AuthenticationStateChanged?.Invoke(true);
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public Task LogoutAsync()
        {
            _currentToken = null;
            _tokenExpiration = default;
            _currentUsername = null;
            _currentUser = null;

            AuthenticationStateChanged?.Invoke(false);
            return Task.CompletedTask;
        }

        public async Task CheckTokenExpirationAsync()
        {
            if (!string.IsNullOrEmpty(_currentToken) && DateTime.UtcNow >= _tokenExpiration)
            {
                await LogoutAsync();
            }
        }

        public async Task<bool> HasPermissionAsync(string permission)
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);
                
                // Buscar claims de "permission" 
                var permissionClaims = jsonToken.Claims
                    .Where(c => c.Type == "permission")
                    .Select(c => c.Value);

                return permissionClaims.Contains(permission);
            }
            catch
            {
                return false;
            }
        }

        private static string GetBaseUrlFromConfig()
        {
            try
            {
                // 1. Primero revisar variable de entorno
                string? envUrl = Environment.GetEnvironmentVariable("TPI_API_BASE_URL");
                if (!string.IsNullOrEmpty(envUrl))
                {
                    return envUrl;
                }

                // 2. Detectar si estamos en Android
                string runtimeInfo = System.Runtime.InteropServices.RuntimeInformation.RuntimeIdentifier;
                if (runtimeInfo.StartsWith("android"))
                {
                    return "http://10.0.2.2:5239/";
                }
            }
            catch
            {
                // Ignorar errores
            }

            // URL por defecto - CORREGIDA para coincidir con launchSettings.json
            return "http://localhost:5239/";
        }
    }
}