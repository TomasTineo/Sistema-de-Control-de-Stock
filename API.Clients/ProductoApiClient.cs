using DTOs.Productos;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class ProductoApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProductoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ProductoDTO?> GetAsync(int id)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/productos/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ProductoDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, "api/productos");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ProductoDTO>>(content, _jsonOptions) 
                ?? new List<ProductoDTO>();
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Post, "api/productos", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(ProductoDTO request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Put, $"api/productos/{request.Id}", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Delete, $"api/productos/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/productos/categoria/{categoriaId}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<ProductoDTO>>(content, _jsonOptions) 
                    ?? new List<ProductoDTO>();
            }

            return new List<ProductoDTO>();
        }
    }
}