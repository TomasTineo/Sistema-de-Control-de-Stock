using DTOs.Productos;

namespace Application.Services.Interfaces
{
    public interface IProductoService
    {
        Task<ProductoDTO?> GetAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> CreateAsync(CreateProductoRequest request);
        Task<bool> UpdateAsync(ProductoDTO request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId);
    }
}
