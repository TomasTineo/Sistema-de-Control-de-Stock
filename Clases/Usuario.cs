using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public string Apellido { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string Salt { get; private set; }

        // Simplificación: Usuario tiene UN grupo (relación simple)
        public int? GrupoPermisoId { get; private set; }
        public virtual GrupoPermiso? Grupo { get; private set; }

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
            if (password.Length < 6)
                throw new ArgumentException("La contraseña debe tener al menos 6 caracteres.", nameof(password));
            Salt = GenerateSalt();
            PasswordHash = HashPassword(password, Salt);
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            string hashedInput = HashPassword(password, Salt);
            return PasswordHash == hashedInput;
        }

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            RandomNumberGenerator.Fill(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using var pbkdf2 = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), 10000, HashAlgorithmName.SHA256);
            byte[] hashBytes = pbkdf2.GetBytes(32);
            return Convert.ToBase64String(hashBytes);
        }

        public override string ToString()
        {
            return $"{Id} - {Nombre} {Apellido}";
        }

        // Métodos para manejo de grupo y permisos (simplificado a UN grupo)
        public void SetGrupo(GrupoPermiso? grupo)
        {
            Grupo = grupo;
            GrupoPermisoId = grupo?.Id;
        }

        public bool TienePermiso(string nombrePermiso)
        {
            if (Grupo == null || !Grupo.Activo)
                return false;

            return Grupo.TienePermiso(nombrePermiso);
        }

        public IEnumerable<string> ObtenerTodosLosPermisos()
        {
            if (Grupo == null || !Grupo.Activo)
                return new List<string>();

            return Grupo.ObtenerNombresPermisos();
        }

        public string? ObtenerNombreGrupo()
        {
            return Grupo?.Nombre;
        }
    }

}

