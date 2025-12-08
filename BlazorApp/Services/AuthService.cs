using System.Net.Http.Json;

namespace BlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7001/");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<bool> Login(string username, string password)
        {
            try
            {
                Console.WriteLine($"🔐 Intentando login: {username}");

                // PRIMERO: Prueba simple - NO LLAMAR A LA API
                if (username == "admin" && password == "123")
                {
                    Console.WriteLine("✅ Login exitoso (prueba)");
                    return true;
                }

                Console.WriteLine("❌ Credenciales incorrectas (prueba)");
                return false;

                // SEGUNDO: Después de que funcione, descomenta esto:
                /*
                var loginData = new { Username = username, Password = password };
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginData);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Login exitoso (API)");
                    return true;
                }
                
                Console.WriteLine("❌ Login fallido (API)");
                return false;
                */
            }
            catch (Exception ex)
            {
                Console.WriteLine($"💥 Error en AuthService.Login: {ex.Message}");
                return false;
            }
        }
    }
}