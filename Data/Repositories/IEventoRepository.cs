using Domain.Model;

namespace Data.Repositories
{
    public interface IEventoRepository
    {
        Task<Evento?> GetAsync(int id);
        Task<IEnumerable<Evento>> GetAllAsync();
        Task<Evento> AddAsync(Evento evento);
        Task<bool> UpdateAsync(Evento evento);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Evento>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}