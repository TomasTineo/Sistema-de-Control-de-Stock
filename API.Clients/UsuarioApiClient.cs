using DTOs;
using DTOs.Usuarios;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace API.Clients
{
    public class UsuarioApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public UsuarioApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        private async Task AddAuthTokenAsync()
        {
            var authService = AuthServiceProvider.Instance;
            await authService.CheckTokenExpirationAsync();
            
            var token = await authService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = 
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<UsuarioDTO?> LoginAsync(string username, string password)
        {
            var loginRequest = new { Username = username, Password = password };
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios/login", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsuarioDTO>(responseContent, _jsonOptions);
            }

            return null;
        }

        public async Task<UsuarioDTO> CreateAsync(CreateUsuarioRequest request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios", content);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UsuarioDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/usuarios/{id}");
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsuarioDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync("api/usuarios");
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UsuarioDTO>>(content, _jsonOptions) ?? new List<UsuarioDTO>();
        }

        public async Task<bool> UpdateAsync(UpdateUsuarioRequest request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/usuarios/{request.Id}", content);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExisteUsernameAsync(string username)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/usuarios/exists/{username}");
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(content, _jsonOptions);
            }

            return false;
        }
    }
}