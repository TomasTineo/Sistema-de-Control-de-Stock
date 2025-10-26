using DTOs.Eventos;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace API.Clients
{
    public class EventoApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public EventoApiClient(HttpClient httpClient)
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

        public async Task<EventoDTO?> GetAsync(int id)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/eventos/{id}");
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EventoDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<EventoDTO>> GetAllAsync()
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync("api/eventos");
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventoDTO>>(content, _jsonOptions) ?? new List<EventoDTO>();
        }

        public async Task<EventoDTO> CreateAsync(CreateEventoRequest request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/eventos", content);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(EventoDTO request)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/eventos/{request.Id}", content);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            await AddAuthTokenAsync();
            
            var response = await _httpClient.DeleteAsync($"api/eventos/{id}");
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            await AddAuthTokenAsync();
            
            var response = await _httpClient.GetAsync($"api/eventos/rango?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EventoDTO>>(content, _jsonOptions) ?? new List<EventoDTO>();
            }

            return new List<EventoDTO>();
        }
    }
}