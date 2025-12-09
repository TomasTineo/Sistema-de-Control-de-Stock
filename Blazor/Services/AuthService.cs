using Blazor.Services;
using DTOs.Auth;
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Blazor.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenStorage _tokenStorage;

        public AuthService(IHttpClientFactory httpClientFactory, ITokenStorage tokenStorage)
        {
            _httpClientFactory = httpClientFactory;
            _tokenStorage = tokenStorage;
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var httpClient = _httpClientFactory.CreateClient("AuthAPI");
            try
            {
                var loginData = new { Username = username, Password = password };
                var jsonContent = JsonSerializer.Serialize(loginData);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error en login: {response.StatusCode}");
                }

                var responseJson = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseJson, options);

                if (loginResponse != null && !string.IsNullOrEmpty(loginResponse.Token))
                {
                    await _tokenStorage.SaveTokenAsync(loginResponse.Token);
                }

                return loginResponse;
            }
            catch (Exception ex)
            {
                // Log del error 
                throw new Exception($"Error durante la autenticación: {ex.Message}", ex);
            }
        }

        public async Task LogoutAsync()
        {
            await _tokenStorage.RemoveTokenAsync();
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var token = await _tokenStorage.GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }

        public async Task<string> GetTokenAsync()
        {
            return await _tokenStorage.GetTokenAsync();
        }
    }
}