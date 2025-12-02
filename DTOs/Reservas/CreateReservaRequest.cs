namespace DTOs.Reservas
{
    public class CreateReservaRequest
    {
        public int ClienteId { get; set; }
        public int EventoId { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public List<CreateReservaProductoRequest> Productos { get; set; } = new List<CreateReservaProductoRequest>();
    }

    public class CreateReservaProductoRequest
    {
        public int ProductoId { get; set; }
        public int CantidadReservada { get; set; }
    }
}
