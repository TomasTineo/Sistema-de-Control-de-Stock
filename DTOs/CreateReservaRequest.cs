using System;
using System.Collections.Generic;

namespace DTOs
{
    public class CreateReservaRequest
    {
        public int ClienteId { get; set; }
        public int EventoId { get; set; }
        public DateTime FechaReserva { get; set; }
        public string Estado { get; set; } = "Pendiente";
        public List<CreateReservaProductoRequest> Productos { get; set; } = new List<CreateReservaProductoRequest>();
    }

    public class CreateReservaProductoRequest
    {
        public int ProductoId { get; set; }
        public int CantidadReservada { get; set; }
    }
}
