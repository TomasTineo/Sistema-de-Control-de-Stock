// Services/ProductoService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs.Productos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;

        public ProductoService(
             IHttpClientFactory httpClientFactory,
               ITokenStorage tokenStorage,
            IJSRuntime jsRuntime,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;

            // Configurar BaseAddress si no está configurado
            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
            }
        }

        public async Task<List<ProductoDTO>> GetProductosAsync()
        {

            try
            {
                Console.WriteLine("=== DEBUG GET PRODUCTOS ===");

                // 1. Obtener token del ITokenStorage (no de sessionStorage directamente)
                var token = await _tokenStorage.GetTokenAsync();
                Console.WriteLine($"Token obtenido de ITokenStorage: {!string.IsNullOrEmpty(token)}");

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("ERROR: No hay token. Redirigiendo al login...");
                    _navigationManager.NavigateTo("/", true);
                    return new List<ProductoDTO>();
                }

                // 2. Configurar el header Authorization
                _httpClient.DefaultRequestHeaders.Remove("Authorization");
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                Console.WriteLine("Authorization header configurado");

                // 3. Hacer la petición
                var response = await _httpClient.GetAsync("api/productos");

                Console.WriteLine($"Status Code: {response.StatusCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Token inválido o expirado. Limpiando token...");
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    return new List<ProductoDTO>();
                }

                response.EnsureSuccessStatusCode();

                var productos = await response.Content.ReadFromJsonAsync<List<ProductoDTO>>();
                Console.WriteLine($"Productos obtenidos: {productos?.Count ?? 0}");

                return productos ?? new List<ProductoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProductosAsync: {ex.Message}");
                throw;
            }
        }
        

        public async Task<ProductoDTO> GetProductoByIdAsync(int id)
        {
            await AgregarTokenAlRequest();
            var response = await _httpClient.GetAsync($"api/productos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductoDTO>();
        }

        public async Task<ProductoDTO> CreateProductoAsync(ProductoDTO producto)
        {
            await AgregarTokenAlRequest();
            var response = await _httpClient.PostAsJsonAsync("api/productos", producto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductoDTO>();
        }

        // Método corregido - solo recibe ProductoDTO
        public async Task<ProductoDTO> UpdateProductoAsync(ProductoDTO producto)
        {
            await AgregarTokenAlRequest();
            var response = await _httpClient.PutAsJsonAsync($"api/productos/{producto.Id}", producto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductoDTO>();
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            await AgregarTokenAlRequest();
            var response = await _httpClient.DeleteAsync($"api/productos/{id}");
            return response.IsSuccessStatusCode;
        }

        private async Task AgregarTokenAlRequest()
        {
            try
            {
                var token = await GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    if (_httpClient.DefaultRequestHeaders.Authorization != null)
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = null;
                    }
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error agregando token: {ex.Message}");
            }
        }

        private async Task<string> GetTokenAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>(
                    "sessionStorage.getItem", "authToken");
            }
            catch
            {
                return null;
            }
        }
    }
}