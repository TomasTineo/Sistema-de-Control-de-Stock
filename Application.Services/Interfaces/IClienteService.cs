using DTOs.Clientes;

namespace Application.Services.Interfaces
{
    public interface IClienteService
    {
        Task<ClienteDTO?> GetAsync(int id);
        Task<IEnumerable<ClienteDTO>> GetAllAsync();
        Task<ClienteDTO> CreateAsync(CreateClienteRequest request);
        Task<bool> UpdateAsync(UpdateClienteRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
