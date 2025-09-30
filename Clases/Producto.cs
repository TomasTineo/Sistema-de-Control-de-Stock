using System;

namespace Domain.Model
{
    public class Producto
    {
        private int _categoriaId;
        private Categoria? _categoria;

        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        public decimal Precio { get; private set; }
        public string Descripcion { get; private set; } = string.Empty;
        public int Stock { get; private set; }

        public int CategoriaId
        {
            get => _categoria?.Id ?? _categoriaId;
            private set => _categoriaId = value;
        }

        public Categoria? Categoria
        {
            get => _categoria;
            private set
            {
                _categoria = value;
                if (value != null && _categoriaId != value.Id)
                {
                    _categoriaId = value.Id;
                }
            }
        }

        protected Producto() { }

        // Constructor SIN ID - para nuevos objetos
        public Producto(string nombre, decimal precio, string descripcion, int stock, int categoriaId)
        {
            SetNombre(nombre);
            SetPrecio(precio);
            SetDescripcion(descripcion);
            SetStock(stock);
            SetCategoriaId(categoriaId);
        }

        // Constructor CON ID - para cargar desde BD
        public Producto(int id, string nombre, decimal precio, string descripcion, int stock, int categoriaId)
        {
            SetID(id);
            SetNombre(nombre);
            SetPrecio(precio);
            SetDescripcion(descripcion);
            SetStock(stock);
            SetCategoriaId(categoriaId);
        }

        public void SetID(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void SetPrecio(decimal precio)
        {
            if (precio < 0)
                throw new ArgumentException("El precio debe ser un valor numérico positivo.", nameof(precio));
            Precio = precio;
        }

        public void SetDescripcion(string descripcion)
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción no puede estar vacía.", nameof(descripcion));
            Descripcion = descripcion;
        }

        public void SetStock(int stock)
        {
            if (stock < 0)
                throw new ArgumentException("El stock debe ser un valor numérico positivo.", nameof(stock));
            Stock = stock;
        }

        public void SetCategoriaId(int categoriaId)
        {
            if (categoriaId <= 0)
                throw new ArgumentException("El ID de la categoría debe ser un número positivo.", nameof(categoriaId));
            CategoriaId = categoriaId;
        }
    }
}

