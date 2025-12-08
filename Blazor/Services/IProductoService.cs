using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs.Productos;

namespace Blazor.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> GetProductosAsync();
        Task<ProductoDTO> GetProductoByIdAsync(int id);
        Task<ProductoDTO> CreateProductoAsync(CreateProductoRequest producto);
        Task<bool> UpdateProductoAsync(int id, ProductoDTO producto);
        Task<bool> DeleteProductoAsync(int id);
    }
}