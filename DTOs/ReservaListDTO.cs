using System;

namespace DTOs
{
    public class ReservaListDTO
    {
        public int Id { get; set; }
        public string NombreCliente { get; set; } = string.Empty;
        public string NombreEvento { get; set; } = string.Empty;
        public DateTime FechaReserva { get; set; }
        public DateTime FechaEvento { get; set; }
        public string Estado { get; set; } = string.Empty;
        public decimal TotalReserva { get; set; }
        public int CantidadTiposProductos { get; set; }
    }
}