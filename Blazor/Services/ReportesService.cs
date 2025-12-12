using Blazor.Auth;
using Blazor.Interfaces;
using DTOs.Reportes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Blazor.Services
{
    public class ReportesService : IReportesService
    {
        private readonly HttpClient _httpClient;
        private readonly IServerTokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public ReportesService(
            IHttpClientFactory httpClientFactory,
            IServerTokenStorage tokenStorage,
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
                Console.WriteLine("=== GET TOP PRODUCTOS RESERVADOS ===");

                await AgregarTokenAlRequest();

                var response = await _httpClient.GetAsync($"api/reportes/top-productos-reservados?top={top}");

                Console.WriteLine($"Status Code: {response.StatusCode}");

                // LEER EL CONTENIDO COMO STRING PRIMERO
                var contentString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"=== CONTENIDO CRUDO ===");
                Console.WriteLine(contentString);
                Console.WriteLine($"=== FIN CONTENIDO ===");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: {contentString}");
                    return new List<TopProductoReservadoDTO>();
                }

                // Intentar deserializar manualmente
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var productos = JsonSerializer.Deserialize<List<TopProductoReservadoDTO>>(contentString, options);

                    Console.WriteLine($"Deserializados {productos?.Count ?? 0} productos");

                    if (productos != null && productos.Any())
                    {
                        Console.WriteLine("Primeros 3 productos:");
                        foreach (var p in productos.Take(3))
                        {
                            Console.WriteLine($"- {p.NombreProducto}: {p.CantidadReservada}");
                        }
                    }

                    return productos ?? new List<TopProductoReservadoDTO>();
                }
                catch (JsonException jsonEx)
                {
                    Console.WriteLine($"ERROR DESERIALIZACIÓN: {jsonEx.Message}");
                    Console.WriteLine($"JSON recibido: {contentString}");
                    return new List<TopProductoReservadoDTO>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
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
