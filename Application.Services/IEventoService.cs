using DTOs;

namespace Application.Services
{
    public interface IEventoService
    {
        Task<EventoDTO?> GetAsync(int id);
        Task<IEnumerable<EventoDTO>> GetAllAsync();
        Task<EventoDTO> CreateAsync(CreateEventoRequest request);
        Task<bool> UpdateAsync(UpdateEventoRequest request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}