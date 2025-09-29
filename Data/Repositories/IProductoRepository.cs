using Domain.Model;

namespace Data.Repositories
{
    public interface IProductoRepository
    {
        Task<Producto?> GetAsync(int id);
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> AddAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Producto>> GetByCategoriaAsync(int categoriaId);
    }
}