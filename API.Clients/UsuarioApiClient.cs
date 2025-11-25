using DTOs;
using DTOs.Usuarios;
using System.Text;
using System.Text.Json;

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
            // No requiere autenticación - es un registro de nuevo usuario
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UsuarioDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/usuarios/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
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
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, "api/usuarios");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UsuarioDTO>>(content, _jsonOptions) ?? new List<UsuarioDTO>();
        }

        public async Task<bool> UpdateAsync(UpdateUsuarioRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Put, $"api/usuarios/{request.Id}", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Delete, $"api/usuarios/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExisteUsernameAsync(string username)
        {
            // No requiere autenticación - endpoint público para validación
            var response = await _httpClient.GetAsync($"api/usuarios/exists/{username}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<bool>(content, _jsonOptions);
            }

            return false;
        }
    }
}