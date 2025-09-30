using Domain.Model;

namespace Data.Repositories
{
    public interface ICategoriaRepository
    {
        Task<Categoria?> GetAsync(int id);
        Task<IEnumerable<Categoria>> GetAllAsync();
        Task<Categoria> AddAsync(Categoria categoria);
        Task<bool> UpdateAsync(Categoria categoria);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExisteNombreAsync(string nombre, int? excludeId = null);
    }
}