using System.Net.Http.Json;
using System.Text;

namespace BlazorApp.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private string? _token;
        private string? _username; // Agregar esta línea

        public AuthService(HttpClient http)
        {
            _http = http;
            Console.WriteLine("AuthService creado");
        }

        public async Task<bool> Login(string username, string password)
        {
            Console.WriteLine("=== AUTH SERVICE LOGIN ===");
            Console.WriteLine($"Usuario: {username}");

            try
            {
                // PRUEBA 1: Primero ver si la API responde
                Console.WriteLine("Probando conexión con API...");
                _http.BaseAddress = new Uri("https://localhost:7001");

                // Hacer una petición GET simple primero
                try
                {
                    var testResponse = await _http.GetAsync("/auth/test");
                    Console.WriteLine($"Test API: {testResponse.StatusCode}");
                }
                catch (Exception apiEx)
                {
                    Console.WriteLine($"✗ API no responde: {apiEx.Message}");
                    return false;
                }

                // PRUEBA 2: Hacer login
                Console.WriteLine("Probando login...");

                // Formato más común
                var loginData = new
                {
                    Email = username,
                    Password = password
                };

                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(loginData),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await _http.PostAsync("/auth/login", content);
                Console.WriteLine($"Login response: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta: {json}");

                    // Buscar token de cualquier forma
                    if (json.Contains("token") || json.Contains("Token"))
                    {
                        _token = "simulated_token_" + DateTime.Now.Ticks;
                        _username = username; // Guardar username
                        Console.WriteLine($"✓ Login exitoso para: {username}");
                        return true;
                    }
                }

                Console.WriteLine("✗ Login falló");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Excepción: {ex.Message}");
                return false;
            }
        }

        public bool IsLoggedIn()
        {
            var logged = !string.IsNullOrEmpty(_token);
            Console.WriteLine($"IsLoggedIn: {logged}");
            return logged;
        }

        // AGREGAR ESTE MÉTODO QUE FALTA
        public string? GetUsername()
        {
            Console.WriteLine($"GetUsername llamado: {_username}");
            return _username;
        }

        public void Logout()
        {
            _token = null;
            _username = null;
            Console.WriteLine("Logout ejecutado");
        }
    }
}