namespace DTOs.Reservas
{
    public class UpdateReservaRequest
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int EventoId { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<UpdateReservaProductoRequest> Productos { get; set; } = new List<UpdateReservaProductoRequest>();
    }

    public class UpdateReservaProductoRequest
    {
        public int ProductoId { get; set; }
        public int CantidadReservada { get; set; }
    }
}
