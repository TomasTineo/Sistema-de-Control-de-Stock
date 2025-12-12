using Microsoft.AspNetCore.Components;
using DTOs.Usuarios;
using Blazor.Interfaces;    

namespace Blazor.Services
{
    public class RegistroService : IRegistroService
    {

        private readonly HttpClient _httpClient;

        public RegistroService(IHttpClientFactory httpClientFactory, NavigationManager navigationManager)
        {
            _httpClient = httpClientFactory.CreateClient("AuthAPI");
        }

        public async Task<CreateUsuarioRequest> CreateUsuario(CreateUsuarioRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/usuarios", request);

            
            if (response.IsSuccessStatusCode)
            {
                var createdUsuario = await response.Content.ReadFromJsonAsync<CreateUsuarioRequest>();
                return createdUsuario!;
            }
            else
            {
                // Leer el mensaje de error del body
                var errorContent = await response.Content.ReadAsStringAsync();

                // Remover las comillas si el mensaje viene entre comillas
                errorContent = errorContent.Trim('"');

                throw new ApplicationException($"{errorContent}");
            }
        }



    }
}
