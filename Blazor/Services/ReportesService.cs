using DTOs.Reportes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Services
{
    public class ReportesService : IReportesService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public ReportesService(
            IHttpClientFactory httpClientFactory,
            ITokenStorage tokenStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _navigationManager = navigationManager;

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
            }
        }

        public async Task<List<ProductoStockDTO>> GetProductosBajoStockAsync(int stockMinimo = 10)
        {
            try
            {
                Console.WriteLine("=== DEBUG GET PRODUCTOS BAJO STOCK ===");
                
                await AgregarTokenAlRequest();

                var response = await _httpClient.GetAsync($"api/reportes/stock-bajo?stockMinimo={stockMinimo}");

                Console.WriteLine($"Status Code: {response.StatusCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Token inválido o expirado. Limpiando token...");
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    return new List<ProductoStockDTO>();
                }

                response.EnsureSuccessStatusCode();

                var productos = await response.Content.ReadFromJsonAsync<List<ProductoStockDTO>>();
                Console.WriteLine($"Productos bajo stock obtenidos: {productos?.Count ?? 0}");

                return productos ?? new List<ProductoStockDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProductosBajoStockAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<List<TopProductoReservadoDTO>> GetTopProductosReservadosAsync(int top = 10)
        {
            try
            {
                Console.WriteLine("=== DEBUG GET TOP PRODUCTOS RESERVADOS ===");
                
                await AgregarTokenAlRequest();

                var response = await _httpClient.GetAsync($"api/reportes/top-productos-reservados?top={top}");

                Console.WriteLine($"Status Code: {response.StatusCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Token inválido o expirado. Limpiando token...");
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    return new List<TopProductoReservadoDTO>();
                }

                response.EnsureSuccessStatusCode();

                var productos = await response.Content.ReadFromJsonAsync<List<TopProductoReservadoDTO>>();
                Console.WriteLine($"Top productos reservados obtenidos: {productos?.Count ?? 0}");

                return productos ?? new List<TopProductoReservadoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetTopProductosReservadosAsync: {ex.Message}");
                throw;
            }
        }

        private async Task AgregarTokenAlRequest()
        {
            try
            {
                var token = await _tokenStorage.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Token no disponible o vacío");
                    return;
                }

                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                Console.WriteLine("Token configurado en header");
            }
            catch (JSException)
            {
                Console.WriteLine("Prerrender detectado - omitiendo token");
            }
            catch (InvalidOperationException ex) when (
                ex.Message.Contains("JavaScript interop") ||
                ex.Message.Contains("statically rendered"))
            {
                Console.WriteLine("Prerrender detectado - omitiendo token");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error configurando token: {ex.Message}");
            }
        }
    }
}
