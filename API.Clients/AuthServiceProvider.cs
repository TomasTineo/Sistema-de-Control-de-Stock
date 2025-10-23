// Singleton pattern
// Propósito: Mantener una única instancia de IAuthService en memoria durante la ejecución de la aplicación.


namespace API.Clients
{
    // Mantiene el token en memoria para toda la aplicación
    // mientras la aplicación esté en ejecución

    public static class AuthServiceProvider
    {
        private static IAuthService? _instance; // aca se almacena la única instancia 
        private static readonly object _lock = new object(); // asegura la sincronización en entornos multihilo

        public static IAuthService Instance // seria el punto de acceso global al servicio
        {
            get
            {
                if (_instance == null) // debe inicializarse antes de usarse
                {
                    throw new InvalidOperationException(
                        "AuthService has not been registered. Call AuthServiceProvider.Register() first.");
                }
                return _instance;
            }
        }

        public static void Register(IAuthService authService)
        {
            lock (_lock)
            {
                if (_instance != null)
                {
                    throw new InvalidOperationException("AuthService is already registered.");
                }
                _instance = authService ?? throw new ArgumentNullException(nameof(authService));
            }
        }

        public static void Clear() // un logout
        {
            lock (_lock)
            {
                _instance = null;
            }
        }
    }
}