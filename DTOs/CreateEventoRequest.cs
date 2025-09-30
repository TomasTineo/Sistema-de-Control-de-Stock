namespace DTOs
{
    public class CreateEventoRequest
    {
        public string NombreEvento { get; set; } = string.Empty;
        public DateTime FechaEvento { get; set; }
    }
}