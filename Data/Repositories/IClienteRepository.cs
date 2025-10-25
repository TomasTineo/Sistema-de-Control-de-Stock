using Domain.Model;

namespace Data.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente?> GetAsync(int id);
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente> AddAsync(Cliente cliente);
        Task<bool> UpdateAsync(Cliente cliente);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
