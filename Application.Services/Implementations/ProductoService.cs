using DTOs.Productos;
using Domain.Model;
using Data.Repositories;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ProductoDTO?> GetAsync(int id)
        {
            var producto = await _productoRepository.GetAsync(id);
            
            if (producto == null) return null;

            return new ProductoDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                CategoriaId = producto.CategoriaId
            };
        }

        public async Task<IEnumerable<ProductoDTO>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            
            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId
            });
        }

        public async Task<ProductoDTO> CreateAsync(CreateProductoRequest request)
        {
            var producto = new Producto(request.Nombre, request.Precio, request.Descripcion, request.Stock, request.CategoriaId);
            
            var productoCreado = await _productoRepository.AddAsync(producto);

            return new ProductoDTO
            {
                Id = productoCreado.Id,
                Nombre = productoCreado.Nombre,
                Precio = productoCreado.Precio,
                Descripcion = productoCreado.Descripcion,
                Stock = productoCreado.Stock,
                CategoriaId = productoCreado.CategoriaId
            };
        }

        public async Task<bool> UpdateAsync(ProductoDTO request)
        {
            var producto = await _productoRepository.GetAsync(request.Id);
            if (producto == null) return false;

            producto.SetNombre(request.Nombre);
            producto.SetPrecio(request.Precio);
            producto.SetDescripcion(request.Descripcion);
            producto.SetStock(request.Stock);
            producto.SetCategoriaId(request.CategoriaId);

            return await _productoRepository.UpdateAsync(producto);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProductoDTO>> GetByCategoriaAsync(int categoriaId)
        {
            var productos = await _productoRepository.GetByCategoriaAsync(categoriaId);
            
            return productos.Select(p => new ProductoDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                Stock = p.Stock,
                CategoriaId = p.CategoriaId
            });
        }
    }
}
