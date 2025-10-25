using DTOs.Categorias;
using Domain.Model;
using Data.Repositories;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaDTO?> GetAsync(int id)
        {
            var categoria = await _categoriaRepository.GetAsync(id);
            
            if (categoria == null) return null;

            return new CategoriaDTO
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            
            return categorias.Select(c => new CategoriaDTO
            {
                Id = c.Id,
                Nombre = c.Nombre
            });
        }

        public async Task<CategoriaDTO> CreateAsync(CreateCategoriaRequest request)
        {
            // Validar que no exista el nombre
            if (await _categoriaRepository.ExisteNombreAsync(request.Nombre))
                throw new InvalidOperationException("Ya existe una categoría con ese nombre");

            // Constructor SIN ID - EF asignará automáticamente
            var categoria = new Categoria(request.Nombre);
            
            var categoriaCreada = await _categoriaRepository.AddAsync(categoria);

            return new CategoriaDTO
            {
                Id = categoriaCreada.Id,
                Nombre = categoriaCreada.Nombre
            };
        }

        public async Task<bool> UpdateAsync(CategoriaDTO request)
        {
            var categoria = await _categoriaRepository.GetAsync(request.Id);
            if (categoria == null) return false;

            // Validar nombre único (excluyendo el actual)
            if (await _categoriaRepository.ExisteNombreAsync(request.Nombre, request.Id))
                throw new InvalidOperationException("Ya existe una categoría con ese nombre");

            // Actualizar propiedades
            categoria.SetNombre(request.Nombre);

            return await _categoriaRepository.UpdateAsync(categoria);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _categoriaRepository.DeleteAsync(id);
        }

        public async Task<bool> ExisteNombreAsync(string nombre)
        {
            return await _categoriaRepository.ExisteNombreAsync(nombre);
        }
    }
}
