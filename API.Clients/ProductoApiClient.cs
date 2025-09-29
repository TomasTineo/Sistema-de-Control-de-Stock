using DTOs;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class ProductoApiClient : IProductoApiClient
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
            var response = await _httpClient.GetAsync($"api/productos/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ProductoDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/productos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ProductoDTO>>(content, _jsonOptions) ?? new List<ProductoDTO>();
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/productos", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(UpdateProductoRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/productos/{request.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId)
        {
            var response = await _httpClient.GetAsync($"api/productos/categoria/{categoriaId}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<ProductoDTO>>(content, _jsonOptions) ?? new List<ProductoDTO>();
            }

            return new List<ProductoDTO>();
        }
    }
}