using DTOs.Productos;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

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

        // Método helper simple para agregar el token
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

        public async Task<ProductoDTO?> GetAsync(int id)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/productos/{id}");
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
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync("api/productos");
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ProductoDTO>>(content, _jsonOptions) 
                ?? new List<ProductoDTO>();
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoRequest request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/productos", content);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProductoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(ProductoDTO request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/productos/{request.Id}", content);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/productos/categoria/{categoriaId}");
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