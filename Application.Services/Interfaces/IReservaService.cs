using DTOs.Reservas;

namespace Application.Services.Interfaces
{
    public interface IReservaService
    {
        Task<ReservaDTO?> GetAsync(int id);
        Task<IEnumerable<ReservaDTO>> GetAllAsync();
        Task<IEnumerable<ReservaDTO>> GetByClienteAsync(int clienteId);
        Task<IEnumerable<ReservaDTO>> GetByEventoAsync(int eventoId);
        Task<IEnumerable<ReservaDTO>> GetByEstadoAsync(string estado);
        Task<IEnumerable<ReservaDTO>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<ReservaDTO> CreateAsync(CreateReservaRequest request);
        Task<bool> UpdateAsync(UpdateReservaRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> CancelarReservaAsync(int id);
        Task<bool> ConfirmarReservaAsync(int id);
    }
}
