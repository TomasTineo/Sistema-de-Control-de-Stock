using DTOs.Clientes;

namespace Blazor.Services
{
    public interface IClienteService
    {
        Task<List<ClienteDTO>> GetClientesAsync();
        Task<ClienteDTO> GetClienteByIdAsync(int id);
        Task<ClienteDTO> CreateClienteAsync(CreateClienteRequest cliente);
        Task<ClienteDTO> UpdateClienteAsync(int id, ClienteDTO cliente);
        Task<bool> DeleteClienteAsync(int id);
    }
}