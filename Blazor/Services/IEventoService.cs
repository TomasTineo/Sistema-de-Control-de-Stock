using System.Collections.Generic;
using System.Threading.Tasks;
using DTOs.Eventos;

namespace Blazor.Services
{
    public interface IEventoService
    {
        Task<List<EventoDTO>> GetEventosAsync();
        Task<EventoDTO> GetEventoByIdAsync(int id);
        Task<EventoDTO> CreateEventoAsync(CreateEventoRequest evento);
        Task<EventoDTO> UpdateEventoAsync(int id, EventoDTO evento);
        Task<bool> DeleteEventoAsync(int id);
    }
}