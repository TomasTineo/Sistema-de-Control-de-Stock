using DTOs;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class UsuarioApiClient : IUsuarioApiClient
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
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/usuarios", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<UsuarioDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/usuarios/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<UsuarioDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/usuarios");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<UsuarioDTO>>(content, _jsonOptions) ?? new List<UsuarioDTO>();
        }

        public async Task<bool> UpdateAsync(UpdateUsuarioRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/usuarios/{request.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/usuarios/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExisteUsernameAsync(string username)
        {
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