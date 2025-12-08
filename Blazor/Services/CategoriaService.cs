using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs.Categorias;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public CategoriaService(
            IHttpClientFactory httpClientFactory,
            ITokenStorage tokenStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<CategoriaDTO>> GetCategoriasAsync()
        {
            try
            {
                await ConfigurarTokenAlRequest();
                var response = await _httpClient.GetAsync("api/categorias");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    return new List<CategoriaDTO>();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<CategoriaDTO>>()
                    ?? new List<CategoriaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetCategoriasAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<CategoriaDTO> GetCategoriaByIdAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync($"api/categorias/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoriaDTO>();
        }

        public async Task<CategoriaDTO> CreateCategoriaAsync(CreateCategoriaRequest categoria)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.PostAsJsonAsync("api/categorias", categoria);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoriaDTO>();
        }

        public async Task<CategoriaDTO> UpdateCategoriaAsync(int id, CategoriaDTO categoria)
        {
            await ConfigurarTokenAlRequest();

            // Asegurar que el ID coincida
            categoria.Id = id;

            Console.WriteLine($"Enviando PUT a: api/categorias/{id}");
            var response = await _httpClient.PutAsJsonAsync($"api/categorias/{id}", categoria);

            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
            Console.WriteLine($"Content-Length: {response.Content.Headers.ContentLength}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new HttpRequestException("Categoría no encontrada", null, System.Net.HttpStatusCode.NotFound);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("Respuesta 204 No Content - devolviendo objeto original");
                // Si la API devuelve 204 sin contenido, devolvemos el objeto que enviamos
                return categoria;
            }

            response.EnsureSuccessStatusCode();

            // Solo intentar leer JSON si hay contenido
            if (response.Content.Headers.ContentLength > 0)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenido de respuesta: {content}");
                return await response.Content.ReadFromJsonAsync<CategoriaDTO>();
            }
            else
            {
                Console.WriteLine("Respuesta sin contenido - devolviendo objeto original");
                return categoria;
            }
        }

        public async Task<bool> DeleteCategoriaAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.DeleteAsync($"api/categorias/{id}");
            return response.IsSuccessStatusCode;
        }

        private async Task ConfigurarTokenAlRequest()
        {
            try
            {
                var token = await _tokenStorage.GetTokenAsync();
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Remove("Authorization");
                    _httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }
            catch
            {
                // Manejar error de token durante prerrender
            }
        }
    }
}