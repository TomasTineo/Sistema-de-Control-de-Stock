using DTOs.Categorias;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class CategoriaApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public CategoriaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<CategoriaDTO?> GetAsync(int id)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/categorias/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CategoriaDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, "api/categorias");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<CategoriaDTO>>(content, _jsonOptions) ?? new List<CategoriaDTO>();
        }

        public async Task<CategoriaDTO> CreateAsync(CreateCategoriaRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Post, "api/categorias", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<CategoriaDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(CategoriaDTO request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Put, $"api/categorias/{request.Id}", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Delete, $"api/categorias/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/categorias/exists/{nombre}");
            var response = await _httpClient.SendAsync(requestMessage);
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