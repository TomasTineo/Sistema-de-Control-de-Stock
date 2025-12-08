using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DTOs.Eventos;
using Microsoft.AspNetCore.Components;

namespace Blazor.Services
{
    public class EventoService : IEventoService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public EventoService(
            IHttpClientFactory httpClientFactory,
            ITokenStorage tokenStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<EventoDTO>> GetEventosAsync()
        {
            try
            {
                await ConfigurarTokenAlRequest();
                var response = await _httpClient.GetAsync("api/eventos");

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await _tokenStorage.RemoveTokenAsync();
                    _navigationManager.NavigateTo("/", true);
                    return new List<EventoDTO>();
                }

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<List<EventoDTO>>()
                    ?? new List<EventoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en GetEventosAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<EventoDTO> GetEventoByIdAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync($"api/eventos/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EventoDTO>();
        }

        public async Task<EventoDTO> CreateEventoAsync(CreateEventoRequest evento)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.PostAsJsonAsync("api/eventos", evento);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EventoDTO>();
        }

        public async Task<EventoDTO> UpdateEventoAsync(int id, EventoDTO evento)
        {
            try
            {
                await ConfigurarTokenAlRequest();

                // Asegurar que el ID coincida
                evento.Id = id;

                Console.WriteLine($"=== DEBUG UPDATE EVENTO ===");
                Console.WriteLine($"ID: {id}");
                Console.WriteLine($"Nombre: {evento.NombreEvento}");
                Console.WriteLine($"Fecha: {evento.FechaEvento}");

                var response = await _httpClient.PutAsJsonAsync($"api/eventos/{id}", evento);

                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Content-Type: {response.Content.Headers.ContentType}");
                Console.WriteLine($"Content-Length: {response.Content.Headers.ContentLength}");

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new HttpRequestException("Evento no encontrado", null, System.Net.HttpStatusCode.NotFound);
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("204 No Content - devolviendo objeto actualizado");
                    // Si la API devuelve 204 sin contenido, devolvemos el objeto que enviamos
                    return evento;
                }

                response.EnsureSuccessStatusCode();

                // Solo intentar leer JSON si hay contenido
                if (response.Content.Headers.ContentLength > 0)
                {
                    return await response.Content.ReadFromJsonAsync<EventoDTO>();
                }
                else
                {
                    Console.WriteLine("Respuesta sin contenido - devolviendo objeto enviado");
                    return evento;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateEventoAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteEventoAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.DeleteAsync($"api/eventos/{id}");
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