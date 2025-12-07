using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs.Productos;

namespace Blazor.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDTO>> GetProductosAsync();
        Task<ProductoDTO> GetProductoByIdAsync(int id);
        Task<ProductoDTO> CreateProductoAsync(ProductoDTO producto);
        Task<ProductoDTO> UpdateProductoAsync(ProductoDTO producto); // Cambiado
        Task<bool> DeleteProductoAsync(int id);
    }
}