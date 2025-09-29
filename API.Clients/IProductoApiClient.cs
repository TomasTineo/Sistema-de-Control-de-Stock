using DTOs;

namespace API.Clients
{
    public interface IProductoApiClient
    {
        Task<ProductoDTO?> GetAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetAllAsync();
        Task<ProductoDTO> CreateAsync(CreateProductoRequest request);
        Task<bool> UpdateAsync(UpdateProductoRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId);
    }
}