using DTOs.Eventos;

namespace Application.Services.Interfaces
{
    public interface IEventoService
    {
        Task<EventoDTO?> GetAsync(int id);
        Task<IEnumerable<EventoDTO>> GetAllAsync();
        Task<EventoDTO> CreateAsync(CreateEventoRequest request);
        Task<bool> UpdateAsync(EventoDTO request);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin);
    }
}
