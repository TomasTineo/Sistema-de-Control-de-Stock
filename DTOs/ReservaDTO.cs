namespace DTOs
{
    public class ReservaDTO
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string EmailCliente { get; set; } = string.Empty;
        public int EventoId { get; set; }
        public string NombreEvento { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = string.Empty;
        public List<ReservaProductoDTO> Productos { get; set; } = new List<ReservaProductoDTO>();
        public decimal TotalReserva { get; set; }
        public int TotalProductos { get; set; }
        public int CantidadTiposProductos { get; set; }
    }
}