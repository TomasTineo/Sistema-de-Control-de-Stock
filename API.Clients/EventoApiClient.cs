using DTOs;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class EventoApiClient : IEventoApiClient
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
            var response = await _httpClient.GetAsync($"api/eventos/{id}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<EventoDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<EventoDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync("api/eventos");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<EventoDTO>>(content, _jsonOptions) ?? new List<EventoDTO>();
        }

        public async Task<EventoDTO> CreateAsync(CreateEventoRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/eventos", content);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<EventoDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(UpdateEventoRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/eventos/{request.Id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/eventos/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var response = await _httpClient.GetAsync($"api/eventos/rango?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<EventoDTO>>(content, _jsonOptions) ?? new List<EventoDTO>();
            }

            return new List<EventoDTO>();
        }
    }
}