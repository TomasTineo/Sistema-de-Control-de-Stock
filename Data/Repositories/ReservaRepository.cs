using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly AppDbContext _context;

        public ReservaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reserva?> GetAsync(int id)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> GetByClienteAsync(int clienteId)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .Where(r => r.ClienteId == clienteId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> GetByEventoAsync(int eventoId)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .Where(r => r.EventoId == eventoId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> GetByEstadoAsync(string estado)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .Where(r => r.Estado == estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reserva>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Evento)
                .Include(r => r.Productos)
                    .ThenInclude(rp => rp.Producto)
                .Where(r => r.FechaReserva >= fechaInicio && r.FechaReserva <= fechaFin)
                .ToListAsync();
        }

        public async Task<Reserva> AddAsync(Reserva reserva)
        {
            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
            
            // Recargar con las relaciones
            return (await GetAsync(reserva.Id))!;
        }

        public async Task<bool> UpdateAsync(Reserva reserva)
        {
            _context.Entry(reserva).State = EntityState.Modified;
            
            // Manejar productos de la reserva
            var productosExistentes = await _context.ReservaProductos
                .Where(rp => rp.ReservaId == reserva.Id)
                .ToListAsync();

            // Eliminar productos que ya no están en la reserva
            var productosAEliminar = productosExistentes
                .Where(pe => !reserva.Productos.Any(p => p.ProductoId == pe.ProductoId))
                .ToList();

            _context.ReservaProductos.RemoveRange(productosAEliminar);

            // Actualizar o agregar productos
            foreach (var producto in reserva.Productos)
            {
                var productoExistente = productosExistentes
                    .FirstOrDefault(pe => pe.ProductoId == producto.ProductoId);

                if (productoExistente != null)
                {
                    // Actualizar
                    productoExistente.SetCantidadReservada(producto.CantidadReservada);
                    _context.Entry(productoExistente).State = EntityState.Modified;
                }
                else
                {
                    // Agregar nuevo
                    _context.ReservaProductos.Add(producto);
                }
            }

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null) return false;

            _context.Reservas.Remove(reserva);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reservas.AnyAsync(r => r.Id == id);
        }
    }
}
