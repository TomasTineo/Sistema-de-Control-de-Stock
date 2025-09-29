using DTOs;

namespace Application.Services
{
    public interface IProductoService
    {
        Task<ProductoDTO?> GetAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> CreateAsync(CreateProductoRequest request);
        Task<bool> UpdateAsync(UpdateProductoRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId);
    }
}