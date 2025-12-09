using DTOs.Reservas;

public interface IReservaService
{
    Task<List<ReservaDTO>> GetReservasAsync();
    Task<List<ReservaListDTO>> GetReservasListAsync(); // Nuevo método
    Task<ReservaDTO> GetReservaByIdAsync(int id);
    Task<ReservaDTO> CreateReservaAsync(CreateReservaRequest reserva);
    Task<ReservaDTO> UpdateReservaAsync(UpdateReservaRequest reserva);
    Task<bool> DeleteReservaAsync(int id);
}