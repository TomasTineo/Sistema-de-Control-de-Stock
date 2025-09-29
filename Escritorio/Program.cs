using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using API.Clients;

namespace Escritorio
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configurar servicios
            var services = new ServiceCollection();

            // HttpClient Factory - API Clients
            services.AddHttpClient<IUsuarioApiClient, UsuarioApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7001/"); // Ajustar según tu puerto
            });

            services.AddHttpClient<IProductoApiClient, ProductoApiClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7001/"); // Ajustar según tu puerto
            });

            // Forms
            services.AddTransient<Form_Acceso>();
            services.AddTransient<Form_Login>();
            services.AddTransient<Form_Registro>();
            services.AddTransient<FormProducts>();

            ServiceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            
            var mainForm = ServiceProvider.GetRequiredService<Form_Acceso>();
            Application.Run(mainForm);
        }
    }
}