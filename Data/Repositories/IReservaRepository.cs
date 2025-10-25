using Domain.Model;

namespace Data.Repositories
{
    public interface IReservaRepository
    {
        Task<Reserva?> GetAsync(int id);
        Task<IEnumerable<Reserva>> GetAllAsync();
        Task<IEnumerable<Reserva>> GetByClienteAsync(int clienteId);
        Task<IEnumerable<Reserva>> GetByEventoAsync(int eventoId);
        Task<IEnumerable<Reserva>> GetByEstadoAsync(string estado);
        Task<IEnumerable<Reserva>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<Reserva> AddAsync(Reserva reserva);
        Task<bool> UpdateAsync(Reserva reserva);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
