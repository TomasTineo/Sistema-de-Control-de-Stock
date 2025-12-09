using DTOs.Reservas;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class ReservaService : IReservaService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public ReservaService(
            IHttpClientFactory httpClientFactory,
            ITokenStorage tokenStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<ReservaDTO>> GetReservasAsync() // Cambiado a ReservaDTO
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync("api/reservas");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _tokenStorage.RemoveTokenAsync();
                _navigationManager.NavigateTo("/", true);
                return new List<ReservaDTO>();
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ReservaDTO>>()
                ?? new List<ReservaDTO>();
        }

        public async Task<List<ReservaListDTO>> GetReservasListAsync()
        {
            await ConfigurarTokenAlRequest();

            // Obtener todas las reservas como DTO completas
            var reservas = await GetReservasAsync();

            // Convertir a ListDTO para la vista
            return reservas.Select(r => new ReservaListDTO
            {
                Id = r.Id,
                NombreCliente = r.Cliente?.Nombre + " " + r.Cliente?.Apellido,
                NombreEvento = r.Evento?.NombreEvento ?? "Sin nombre", // Cambiado de NombreEvento a Nombre
                FechaReserva = r.FechaReserva,
                FechaEvento = r.Evento?.FechaEvento ?? DateTime.MinValue, // Cambiado de FechaEvento a Fecha
                Estado = r.Estado,
                TotalReserva = r.TotalReserva,
                CantidadTiposProductos = r.Productos?.Count ?? 0
            }).ToList();
        }

        public async Task<ReservaDTO> GetReservaByIdAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync($"api/reservas/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReservaDTO>();
        }

        public async Task<ReservaDTO> CreateReservaAsync(CreateReservaRequest reserva)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.PostAsJsonAsync("api/reservas", reserva);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ReservaDTO>();
        }

        public async Task<ReservaDTO> UpdateReservaAsync(UpdateReservaRequest reserva)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.PutAsJsonAsync($"api/reservas/{reserva.Id}", reserva);
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return null; // O manejar según tu API

            return await response.Content.ReadFromJsonAsync<ReservaDTO>();
        }

        public async Task<bool> DeleteReservaAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.DeleteAsync($"api/reservas/{id}");
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