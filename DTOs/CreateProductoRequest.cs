namespace DTOs
{
    public class CreateProductoRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
    }
}