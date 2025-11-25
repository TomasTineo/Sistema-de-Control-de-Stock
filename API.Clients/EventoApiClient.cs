using DTOs.Eventos;
using System.Text;
using System.Text.Json;

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

        public async Task<EventoDTO?> GetAsync(int id)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/eventos/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
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
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, "api/eventos");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventoDTO>>(content, _jsonOptions) ?? new List<EventoDTO>();
        }

        public async Task<EventoDTO> CreateAsync(CreateEventoRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Post, "api/eventos", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(EventoDTO request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Put, $"api/eventos/{request.Id}", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Delete, $"api/eventos/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var requestMessage = await CreateAuthenticatedRequest(
                HttpMethod.Get, 
                $"api/eventos/rango?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");
            var response = await _httpClient.SendAsync(requestMessage);
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