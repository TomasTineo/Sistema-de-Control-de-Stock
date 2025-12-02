using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Reserva
    {
        private int _clienteId;
        private Cliente? _cliente;
        private int _eventoId;
        private Evento? _evento;

        // Cambiar de List<> a ICollection<> para EF
        public virtual ICollection<ReservaProducto> Productos { get; private set; } = new List<ReservaProducto>();

        public int Id { get; private set; }

        public int ClienteId 
        { 
            get => _cliente?.Id ?? _clienteId;
            private set => _clienteId = value; 
        }

        public Cliente? Cliente 
        { 
            get => _cliente;
            private set 
            {
                _cliente = value;
                if (value != null && _clienteId != value.Id)
                {
                    _clienteId = value.Id; // Sincronizar automáticamente
                }
            }
        }

        public int EventoId 
        { 
            get => _evento?.Id ?? _eventoId;
            private set => _eventoId = value; 
        }

        public Evento? Evento 
        { 
            get => _evento;
            private set 
            {
                _evento = value;
                if (value != null && _eventoId != value.Id)
                {
                    _eventoId = value.Id; // Sincronizar automáticamente
                }
            }
        }

        public DateTime FechaReserva { get; private set; }
        public DateTime FechaFinalizacion { get; private set; }
        public string Estado { get; private set; }

        // Constructor privado para Entity Framework
        private Reserva() 
        {
            Estado = "Pendiente";
            FechaReserva = DateTime.Now;
        }

        // Constructor para nuevas reservas (sin ID, lo generará la BD)
        public Reserva(int clienteId, int eventoId, DateTime fechaFinalizacion, string estado = "Pendiente")
        {
            Productos = new List<ReservaProducto>();
            FechaReserva = DateTime.Now; // La fecha de creación es siempre hoy
            SetClienteId(clienteId);
            SetEventoId(eventoId);
            SetFechaFinalizacion(fechaFinalizacion);
            SetEstado(estado);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            Id = id;
        }

        public void SetClienteId(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("El ID del cliente debe ser un número positivo.", nameof(clienteId));
            ClienteId = clienteId;
        }

        public void SetEventoId(int eventoId)
        {
            if (eventoId <= 0)
                throw new ArgumentException("El ID del evento debe ser un número positivo.", nameof(eventoId));
            EventoId = eventoId;
        }

        public void SetFechaReserva(DateTime fechaReserva)
        {
            if (fechaReserva == default(DateTime))
                throw new ArgumentException("La fecha de reserva no puede estar vacía.", nameof(fechaReserva));
            
            // Permitir fechas pasadas para edición de reservas existentes
            FechaReserva = fechaReserva;
        }

        public void SetFechaFinalizacion(DateTime fechaFinalizacion)
        {
            if (fechaFinalizacion == default(DateTime))
                throw new ArgumentException("La fecha de finalización no puede estar vacía.", nameof(fechaFinalizacion));
            
            // La fecha de finalización debe ser posterior a la fecha de reserva
            if (fechaFinalizacion <= FechaReserva)
                throw new ArgumentException("La fecha de finalización debe ser posterior a la fecha de reserva.", nameof(fechaFinalizacion));
            
            FechaFinalizacion = fechaFinalizacion;
        }

        public void SetEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado no puede estar vacío.", nameof(estado));
            
            var estadosValidos = new[] { "Pendiente", "Confirmada", "Entregada", "Cancelada", "Completada" };
            if (!estadosValidos.Contains(estado))
                throw new ArgumentException("El estado debe ser: Pendiente, Confirmada, Entregada, Cancelada o Completada.", nameof(estado));
            
            Estado = estado;
        }

        // Métodos para manejar productos
        public void AgregarProducto(int productoId, int cantidad)
        {
            if (Estado == "Cancelada")
                throw new InvalidOperationException("No se pueden agregar productos a una reserva cancelada.");

            var productoExistente = Productos.FirstOrDefault(p => p.ProductoId == productoId);
            
            if (productoExistente != null)
            {
                // Si ya existe, actualizar cantidad
                productoExistente.SetCantidadReservada(productoExistente.CantidadReservada + cantidad);
            }
            else
            {
                // Si no existe, agregarlo
                Productos.Add(new ReservaProducto(Id, productoId, cantidad));
            }
        }

        public void RemoverProducto(int productoId)
        {
            if (Estado == "Cancelada")
                throw new InvalidOperationException("No se pueden remover productos de una reserva cancelada.");

            var producto = Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (producto != null)
            {
                Productos.Remove(producto);
            }
        }

        public void ActualizarCantidadProducto(int productoId, int nuevaCantidad)
        {
            if (Estado == "Cancelada")
                throw new InvalidOperationException("No se pueden modificar productos de una reserva cancelada.");

            var producto = Productos.FirstOrDefault(p => p.ProductoId == productoId);
            if (producto != null)
            {
                producto.SetCantidadReservada(nuevaCantidad);
            }
            else
            {
                throw new ArgumentException($"No existe un producto con ID {productoId} en esta reserva.");
            }
        }

        public void LimpiarProductos()
        {
            Productos.Clear();
        }

        // Propiedades calculadas
        public decimal TotalReserva => Productos.Sum(p => p.SubTotal);
        public int TotalProductos => Productos.Sum(p => p.CantidadReservada);
        public int CantidadTiposProductos => Productos.Count;
    }
}
