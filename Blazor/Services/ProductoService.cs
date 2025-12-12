// Services/ProductoService.cs
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazor.Auth;
using Blazor.Interfaces;
using DTOs.Productos;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Services
{
    public class ProductoService : IProductoService
    {
        private readonly HttpClient _httpClient;
        private readonly IServerTokenStorage _tokenStorage;

        private readonly NavigationManager _navigationManager;

        public ProductoService(
            IHttpClientFactory httpClientFactory,
            IServerTokenStorage tokenStorage,
            IJSRuntime jsRuntime,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;

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

                await AgregarTokenAlRequest();

                // Hacer la petición
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
            try
            {
                
                await AgregarTokenAlRequest();

                Console.WriteLine($"Obteniendo producto con ID: {id}");
                var response = await _httpClient.GetAsync($"api/productos/{id}");

                Console.WriteLine($"Status Code GetById: {response.StatusCode}");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("No autorizado en GetProductoByIdAsync");
                    
                    throw new HttpRequestException("No autorizado", null, System.Net.HttpStatusCode.Unauthorized);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine($"Producto {id} no encontrado");
                    return null;
                }

                response.EnsureSuccessStatusCode();

                var producto = await response.Content.ReadFromJsonAsync<ProductoDTO>();
                Console.WriteLine($"Producto obtenido: {producto?.Nombre}");

                return producto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProductoByIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductoDTO> CreateProductoAsync(CreateProductoRequest producto)
        {

            try
            {
                Console.WriteLine("=== DEBUG CREAR PRODUCTOS ===");

                await AgregarTokenAlRequest();

                var response = await _httpClient.PostAsJsonAsync("api/productos", producto);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    throw new UnauthorizedAccessException("No autorizado");
                }


                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ProductoDTO>();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetProductosAsync: {ex.Message}");
                throw;
            }

        }


        public async Task<bool> UpdateProductoAsync(int id, ProductoDTO producto)
        {
            await AgregarTokenAlRequest();

            producto.Id = id;

            Console.WriteLine("=== DEBUG UPDATE PRODUCTO ===");
            Console.WriteLine($"ID: {producto.Id}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Precio: {producto.Precio}");
            Console.WriteLine($"Descripción: {producto.Descripcion}");
            Console.WriteLine($"Stock: {producto.Stock}");
            Console.WriteLine($"CategoríaId: {producto.CategoriaId}");

            // Serializar manualmente para ver el JSON
            var json = System.Text.Json.JsonSerializer.Serialize(producto);
            Console.WriteLine($"JSON enviado: {json}");

            var response = await _httpClient.PutAsJsonAsync($"api/productos/{id}", producto);

            Console.WriteLine($"Status Code: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error content: {errorContent}");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            response.EnsureSuccessStatusCode();
            return true;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            try
            {
                await AgregarTokenAlRequest();
                var response = await _httpClient.DeleteAsync($"api/productos/{id}");

                // Verificar específicamente el código 409 Conflict
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error 409 Conflict: {errorContent}");
                    
                    // Lanzar HttpRequestException con el StatusCode
                    throw new HttpRequestException(
                        "No se puede eliminar el producto porque tiene reservas asociadas.",
                        null,
                        System.Net.HttpStatusCode.Conflict);
                }

                // Verificar otros códigos de error
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"DeleteProductoAsync - Código: {(int)response.StatusCode} - {response.StatusCode}");
                    response.EnsureSuccessStatusCode();
                }

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                // Re-lanzar HttpRequestException tal como viene
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción general en DeleteProductoAsync: {ex.Message}");
                throw;
            }
        }

        private async Task AgregarTokenAlRequest()
        {
            try
            {
                // Intenta obtener el token
                var token = await _tokenStorage.GetTokenAsync();

                if (string.IsNullOrEmpty(token))
                {
                    Console.WriteLine("Token no disponible o vacío");
                    return; // No configurar header si no hay token
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