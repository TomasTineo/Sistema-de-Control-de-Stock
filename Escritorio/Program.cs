using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using API.Clients;
using API.Auth.WindowsForm;

namespace Escritorio
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        [STAThread]
        static async Task Main()
        {
            // Registrar AuthService PRIMERO (antes de DI)
            var authService = new WindowsFormsAuthService();
            AuthServiceProvider.Register(authService);

            // Configurar servicios
            var services = new ServiceCollection();

            // Configuración
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // HttpClient Factory con configuración desde appsettings
            var apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5239/";

            // Registrar ApiClients con HttpClient (sin AuthHeaderHandler)
            services.AddHttpClient<UsuarioApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<ProductoApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<CategoriaApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<EventoApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<ReservaApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<ClienteApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            services.AddHttpClient<AuthApiClient>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Registrar AuthService como singleton
            services.AddSingleton<IAuthService>(authService);

            // Forms
            services.AddTransient<Form_Acceso>();
            services.AddTransient<Form_Login>();
            services.AddTransient<Form_Registro>();
            services.AddTransient<Form_Productos>();
            services.AddTransient<Form_Main>();
            services.AddTransient<Form_Categorias>();
            services.AddTransient<Form_Eventos>();
            services.AddTransient<Form_Clientes>();
            services.AddTransient<Form_Reserva>();

            ServiceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();

            // Verificar conexión con la API al inicio
            if (await VerificarConexionApi(configuration))
            {
                var mainForm = ServiceProvider.GetRequiredService<Form_Acceso>();
                Application.Run(mainForm);
            }
            else
            {
                // Mostrar opción de continuar sin API o reintentar
                var result = MessageBox.Show(
                    "No se pudo conectar con la API. ¿Desea continuar sin conexión?\n\n" +
                    "Sí: Continuar sin API\n" +
                    "No: Cerrar aplicación", 
                    "Error de conexión", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var mainForm = ServiceProvider.GetRequiredService<Form_Acceso>();
                    Application.Run(mainForm);
                }
            }
        }

        private static async Task<bool> VerificarConexionApi(IConfiguration configuration)
        {
            try
            {
                var apiBaseUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5239/";
                
                // Intentar HTTPS primero
                if (await TryConnectToApi(apiBaseUrl))
                    return true;
                
                // Si HTTPS falla, intentar con HTTP
                if (apiBaseUrl.StartsWith("https://"))
                {
                    var httpUrl = apiBaseUrl.Replace("https://", "http://").Replace(":7001/", ":5239/");
                    
                    if (await TryConnectToApi(httpUrl))
                    {
                        MessageBox.Show($"API encontrada en: {httpUrl}", 
                                      "Conexión establecida", 
                                      MessageBoxButtons.OK, 
                                      MessageBoxIcon.Information);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar conexión: {ex.Message}",
                              "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private static async Task<bool> TryConnectToApi(string baseUrl)
        {
            try
            {
                // Crear un NUEVO HttpClient para cada intento
                using var httpClient = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl),
                    Timeout = TimeSpan.FromSeconds(10)
                };

                string[] endpoints = { "api/health", "swagger/index.html", "" };
                
                foreach (var endpoint in endpoints)
                {
                    try
                    {
                        var response = await httpClient.GetAsync(endpoint);
                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                    }
                    catch (HttpRequestException)
                    {
                        // Continuar con el siguiente endpoint
                        continue;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}