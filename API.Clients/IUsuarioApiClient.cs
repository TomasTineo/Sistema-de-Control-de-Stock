using DTOs;

namespace API.Clients
{
    public interface IUsuarioApiClient
    {
        Task<UsuarioDTO?> LoginAsync(string username, string password);
        Task<UsuarioDTO> CreateAsync(CreateUsuarioRequest request);
        Task<UsuarioDTO?> GetAsync(int id);
        Task<IEnumerable<UsuarioDTO>> GetAllAsync();
        Task<bool> UpdateAsync(UpdateUsuarioRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExisteUsernameAsync(string username);
    }
}