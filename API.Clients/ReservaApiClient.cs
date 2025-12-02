using DTOs.Reservas;
using System.Text;
using System.Text.Json;

namespace API.Clients
{
    public class ReservaApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ReservaApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<ReservaDTO?> GetAsync(int id)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/reservas/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ReservaDTO>(content, _jsonOptions);
            }

            return null;
        }

        public async Task<IEnumerable<ReservaDTO>> GetAllAsync()
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, "api/reservas");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<ReservaDTO>>(content, _jsonOptions) ?? new List<ReservaDTO>();
        }

        public async Task<ReservaDTO> CreateAsync(CreateReservaRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Post, "api/reservas", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ReservaDTO>(responseContent, _jsonOptions)!;
        }

        public async Task<bool> UpdateAsync(UpdateReservaRequest request)
        {
            await EnsureAuthenticatedAsync();
            
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Put, $"api/reservas/{request.Id}", content);
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await EnsureAuthenticatedAsync();
            
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Delete, $"api/reservas/{id}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ReservaDTO>> GetByClienteAsync(int clienteId)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/reservas/cliente/{clienteId}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<ReservaDTO>>(content, _jsonOptions) ?? new List<ReservaDTO>();
            }

            return new List<ReservaDTO>();
        }

        public async Task<IEnumerable<ReservaDTO>> GetByEventoAsync(int eventoId)
        {
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/reservas/evento/{eventoId}");
            var response = await _httpClient.SendAsync(requestMessage);
            await HandleUnauthorizedResponseAsync(response);
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<ReservaDTO>>(content, _jsonOptions) ?? new List<ReservaDTO>();
            }

            return new List<ReservaDTO>();
        }
    }
}
