using System;

namespace Domain.Model
{
    public class ReservaProducto
    {
        private int _productoId;
        private Producto? _producto;

        public int ReservaId { get; private set; } // Clave foránea a Reserva

        public int ProductoId 
        { 
            get => _producto?.Id ?? _productoId;
            private set => _productoId = value; 
        }

        public Producto? Producto 
        { 
            get => _producto;
            private set 
            {
                _producto = value;
                if (value != null && _productoId != value.Id)
                {
                    _productoId = value.Id;
                }
            }
        }

        public int CantidadReservada { get; private set; }

        protected ReservaProducto() { }

        public ReservaProducto(int reservaId, int productoId, int cantidadReservada)
        {
            SetReservaId(reservaId);
            SetProductoId(productoId);
            SetCantidadReservada(cantidadReservada);
        }

        public void SetReservaId(int reservaId)
        {
            if (reservaId <= 0)
                throw new ArgumentException("El ID de la reserva debe ser un número positivo.", nameof(reservaId));
            ReservaId = reservaId;
        }

        public void SetProductoId(int productoId)
        {
            if (productoId <= 0)
                throw new ArgumentException("El ID del producto debe ser un número positivo.", nameof(productoId));
            ProductoId = productoId;
        }

        public void SetCantidadReservada(int cantidadReservada)
        {
            if (cantidadReservada <= 0)
                throw new ArgumentException("La cantidad reservada debe ser mayor a cero.", nameof(cantidadReservada));
            CantidadReservada = cantidadReservada;
        }

        // Propiedades de conveniencia
        public string NombreProducto => Producto?.Nombre ?? "Producto no encontrado";
        public decimal PrecioUnitario => Producto?.Precio ?? 0;
        public decimal SubTotal => PrecioUnitario * CantidadReservada;
    }
}