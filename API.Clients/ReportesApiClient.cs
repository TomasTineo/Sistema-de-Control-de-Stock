using DTOs.Reportes;
using System.Net.Http.Json;

namespace API.Clients
{
    public class ReportesApiClient : BaseApiClient
    {
        private readonly HttpClient _httpClient;

        public ReportesApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtiene productos con stock bajo el mínimo especificado
        /// </summary>
        public async Task<IEnumerable<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo = 10)
        {
            await EnsureAuthenticatedAsync();
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/reportes/stock-bajo?stockMinimo={stockMinimo}");
            var response = await _httpClient.SendAsync(requestMessage);

            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<ProductoStockDTO>>()
                ?? new List<ProductoStockDTO>();
        }

        /// <summary>
        /// Obtiene el top de productos más reservados
        /// </summary>
        public async Task<IEnumerable<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top = 10)
        {
            await EnsureAuthenticatedAsync();
            var requestMessage = await CreateAuthenticatedRequest(HttpMethod.Get, $"api/reportes/top-productos-reservados?top={top}");
            var response = await _httpClient.SendAsync(requestMessage);

            await HandleUnauthorizedResponseAsync(response);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<IEnumerable<TopProductoReservadoDTO>>()
                ?? new List<TopProductoReservadoDTO>();
        }
    }
}
