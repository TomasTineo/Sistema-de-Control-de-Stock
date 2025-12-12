using DTOs.Usuarios;

namespace Blazor.Interfaces
{
    public interface IRegistroService
    {
        Task<CreateUsuarioRequest> CreateUsuario(CreateUsuarioRequest request);
    }
}
