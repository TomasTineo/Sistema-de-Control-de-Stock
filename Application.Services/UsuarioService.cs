using DTOs;
using Domain.Model;
using Data.Repositories;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDTO?> LoginAsync(string username, string password)
        {
            var usuario = await _usuarioRepository.GetByUsernameAsync(username);
            
            if (usuario == null || usuario.PasswordHash != password)
                return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.Username
            };
        }

        public async Task<UsuarioDTO> CreateAsync(CreateUsuarioRequest request)
        {
            // Validar que no exista el username
            if (await _usuarioRepository.ExisteUsernameAsync(request.Username))
                throw new InvalidOperationException("El nombre de usuario ya existe");

            // Constructor SIN ID - EF asignará automáticamente
            var usuario = new Usuario(request.Nombre, request.Apellido, request.Email, request.Username, request.Password);
            
            var usuarioCreado = await _usuarioRepository.AddAsync(usuario);

            return new UsuarioDTO
            {
                Id = usuarioCreado.Id, //  Ahora tendrá el ID asignado por la BD
                Nombre = usuarioCreado.Nombre,
                Apellido = usuarioCreado.Apellido,
                Email = usuarioCreado.Email,
                Username = usuarioCreado.Username
            };
        }

        public async Task<UsuarioDTO?> GetAsync(int id)
        {
            var usuario = await _usuarioRepository.GetAsync(id);
            
            if (usuario == null) return null;

            return new UsuarioDTO
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Username = usuario.Username
            };
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            
            return usuarios.Select(u => new UsuarioDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellido = u.Apellido,
                Email = u.Email,
                Username = u.Username
            });
        }

        public async Task<bool> UpdateAsync(UpdateUsuarioRequest request)
        {
            var usuario = await _usuarioRepository.GetAsync(request.Id);
            if (usuario == null) return false;

            // Validar username único (excluyendo el actual)
            if (await _usuarioRepository.ExisteUsernameAsync(request.Username, request.Id))
                throw new InvalidOperationException("El nombre de usuario ya existe");

            // Actualizar propiedades
            usuario.SetNombre(request.Nombre);
            usuario.SetApellido(request.Apellido);
            usuario.SetEmail(request.Email);
            usuario.SetUsername(request.Username);
            
            if (!string.IsNullOrEmpty(request.Password))
                usuario.SetPassword(request.Password);

            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _usuarioRepository.DeleteAsync(id);
        }

        public async Task<bool> ExisteUsernameAsync(string username)
        {
            return await _usuarioRepository.ExisteUsernameAsync(username);
        }
    }
}