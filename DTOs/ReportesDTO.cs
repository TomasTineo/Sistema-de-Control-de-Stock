namespace DTOs
{
    // DTO para el Reporte 1: Productos con bajo stock.
    public class ProductoStockDTO
    {
        public string NombreProducto { get; set; }
        public int StockActual { get; set; }
    }

    // DTO para el Reporte 2: Reservas por mes.
    public class ReservasPorMesDTO
    {
        public int MesNumero { get; set; }
        public string NombreMes { get; set; }
        public int TotalReservas { get; set; }
    }
}
