using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Evento?> GetAsync(int id)
        {
            return await _context.Eventos.FindAsync(id);
        }

        public async Task<IEnumerable<Evento>> GetAllAsync()
        {
            return await _context.Eventos.OrderBy(e => e.FechaEvento).ToListAsync();
        }

        public async Task<Evento> AddAsync(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }

        public async Task<bool> UpdateAsync(Evento evento)
        {
            _context.Entry(evento).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null) return false;

            _context.Eventos.Remove(evento);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Evento>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            return await _context.Eventos
                .Where(e => e.FechaEvento >= fechaInicio && e.FechaEvento <= fechaFin)
                .OrderBy(e => e.FechaEvento)
                .ToListAsync();
        }
    }
}