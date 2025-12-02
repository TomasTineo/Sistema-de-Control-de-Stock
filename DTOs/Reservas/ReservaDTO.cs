using DTOs.Clientes;
using DTOs.Eventos;

namespace DTOs.Reservas
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public ClienteDTO? Cliente { get; set; }
        public int EventoId { get; set; }
        public EventoDTO? Evento { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<ReservaProductoDTO> Productos { get; set; } = new List<ReservaProductoDTO>();
        public decimal TotalReserva { get; set; }
        public int TotalProductos { get; set; }
        public int CantidadTiposProductos { get; set; }
    }
}
