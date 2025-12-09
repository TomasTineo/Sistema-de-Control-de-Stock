using DTOs.Clientes;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Blazor.Services
{
    public class ClienteService : IClienteService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenStorage _tokenStorage;
        private readonly NavigationManager _navigationManager;

        public ClienteService(
            IHttpClientFactory httpClientFactory,
            ITokenStorage tokenStorage,
            NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
            _tokenStorage = tokenStorage;
            _navigationManager = navigationManager;
        }

        public async Task<List<ClienteDTO>> GetClientesAsync()
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync("api/clientes");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                await _tokenStorage.RemoveTokenAsync();
                _navigationManager.NavigateTo("/", true);
                return new List<ClienteDTO>();
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ClienteDTO>>()
                ?? new List<ClienteDTO>();
        }

        public async Task<ClienteDTO> GetClienteByIdAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.GetAsync($"api/clientes/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClienteDTO>();
        }

        public async Task<ClienteDTO> CreateClienteAsync(CreateClienteRequest cliente)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.PostAsJsonAsync("api/clientes", cliente);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ClienteDTO>();
        }

        public async Task<ClienteDTO> UpdateClienteAsync(int id, ClienteDTO cliente)
        {
            await ConfigurarTokenAlRequest();
            cliente.Id = id;
            var response = await _httpClient.PutAsJsonAsync($"api/clientes/{id}", cliente);
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return cliente;

            return await response.Content.ReadFromJsonAsync<ClienteDTO>();
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            await ConfigurarTokenAlRequest();
            var response = await _httpClient.DeleteAsync($"api/clientes/{id}");
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