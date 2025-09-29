using System;

namespace DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public string NombreEvento { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
    }
}