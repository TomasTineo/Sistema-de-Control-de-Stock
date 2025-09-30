namespace Domain.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;

        protected Usuario() { }

        // Constructor SIN ID - para nuevos objetos
        public Usuario(string nombre, string apellido, string email, string username, string password)
        {
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(email);
            SetUsername(username);
            SetPassword(password);
        }

        // Constructor CON ID - para cargar desde BD
        public Usuario(int id, string nombre, string apellido, string email, string username, string password)
        {
            SetId(id);
            SetNombre(nombre);
            SetApellido(apellido);
            SetEmail(email);
            SetUsername(username);
            SetPassword(password);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            Id = id;
        }

        // ← Permitir que EF asigne el ID internamente
        internal void SetIdInternal(int id)
        {
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetApellido(string apellido)
        {
            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido no puede estar vacío.", nameof(apellido));
            Apellido = apellido;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email no puede estar vacío.", nameof(email));
            Email = email;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("El username no puede estar vacío.", nameof(username));
            Username = username;
        }

        public void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("La contraseña no puede estar vacía.", nameof(password));
            Password = password;
        }

        public override string ToString()
        {
            return $"{Id} - {Nombre} {Apellido}";
        }
    }
}
