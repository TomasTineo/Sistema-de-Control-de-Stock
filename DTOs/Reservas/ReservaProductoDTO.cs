namespace DTOs.Reservas
{
    public class ReservaProductoDTO
    {
        public int ProductoId { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int CantidadReservada { get; set; }
        public decimal SubTotal { get; set; }
    }
}
