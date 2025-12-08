using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria?> GetAsync(int id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria> AddAsync(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null) return false;

            try
            {
                _context.Categorias.Remove(categoria);
                var result = await _context.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                // La categoría tiene productos asociados
                throw new InvalidOperationException(
                    "No se puede eliminar la categoría porque tiene productos asociados.", ex);
            }
        }

        public async Task<bool> ExisteNombreAsync(string nombre, int? excludeId = null)
        {
            var query = _context.Categorias.Where(c => c.Nombre == nombre);
            
            if (excludeId.HasValue)
                query = query.Where(c => c.Id != excludeId.Value);

            return await query.AnyAsync();
        }
    }
}