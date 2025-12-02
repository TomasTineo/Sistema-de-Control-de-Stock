using DTOs.Reservas;
using DTOs.Clientes;
using DTOs.Eventos;
using Domain.Model;
using Data.Repositories;
using Application.Services.Interfaces;

namespace Application.Services.Implementations
{
    public class ReservaService : IReservaService
    {
        private readonly IReservaRepository _reservaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IProductoRepository _productoRepository;

        public ReservaService(
            IReservaRepository reservaRepository,
            IClienteRepository clienteRepository,
            IEventoRepository eventoRepository,
            IProductoRepository productoRepository)
        {
            _reservaRepository = reservaRepository;
            _clienteRepository = clienteRepository;
            _eventoRepository = eventoRepository;
            _productoRepository = productoRepository;
        }

        public async Task<ReservaDTO?> GetAsync(int id)
        {
            var reserva = await _reservaRepository.GetAsync(id);
            return reserva == null ? null : MapToDTO(reserva);
        }

        public async Task<IEnumerable<ReservaDTO>> GetAllAsync()
        {
            var reservas = await _reservaRepository.GetAllAsync();
            return reservas.Select(MapToDTO);
        }

        public async Task<IEnumerable<ReservaDTO>> GetByClienteAsync(int clienteId)
        {
            var reservas = await _reservaRepository.GetByClienteAsync(clienteId);
            return reservas.Select(MapToDTO);
        }

        public async Task<IEnumerable<ReservaDTO>> GetByEventoAsync(int eventoId)
        {
            var reservas = await _reservaRepository.GetByEventoAsync(eventoId);
            return reservas.Select(MapToDTO);
        }

        public async Task<IEnumerable<ReservaDTO>> GetByEstadoAsync(string estado)
        {
            var reservas = await _reservaRepository.GetByEstadoAsync(estado);
            return reservas.Select(MapToDTO);
        }

        public async Task<IEnumerable<ReservaDTO>> GetByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var reservas = await _reservaRepository.GetByFechaRangoAsync(fechaInicio, fechaFin);
            return reservas.Select(MapToDTO);
        }

        public async Task<ReservaDTO> CreateAsync(CreateReservaRequest request)
        {
            // Validar que el cliente existe
            var cliente = await _clienteRepository.GetAsync(request.ClienteId);
            if (cliente == null)
                throw new InvalidOperationException($"El cliente con ID {request.ClienteId} no existe.");

            // Validar que el evento existe
            var evento = await _eventoRepository.GetAsync(request.EventoId);
            if (evento == null)
                throw new InvalidOperationException($"El evento con ID {request.EventoId} no existe.");

            // Crear la reserva (FechaReserva se establece automáticamente a DateTime.Now en el constructor)
            var reserva = new Reserva(
                request.ClienteId,
                request.EventoId,
                request.FechaFinalizacion,
                request.Estado
            );

            // Agregar productos y validar stock
            foreach (var productoRequest in request.Productos)
            {
                var producto = await _productoRepository.GetAsync(productoRequest.ProductoId);
                if (producto == null)
                    throw new InvalidOperationException($"El producto con ID {productoRequest.ProductoId} no existe.");

                if (producto.Stock < productoRequest.CantidadReservada)
                    throw new InvalidOperationException($"Stock insuficiente para el producto '{producto.Nombre}'. Stock disponible: {producto.Stock}, solicitado: {productoRequest.CantidadReservada}");

                reserva.AgregarProducto(productoRequest.ProductoId, productoRequest.CantidadReservada);
            }

            var reservaCreada = await _reservaRepository.AddAsync(reserva);
            return MapToDTO(reservaCreada);
        }

        public async Task<bool> UpdateAsync(UpdateReservaRequest request)
        {
            var reserva = await _reservaRepository.GetAsync(request.Id);
            if (reserva == null) return false;

            // Validar que el cliente existe
            var cliente = await _clienteRepository.GetAsync(request.ClienteId);
            if (cliente == null)
                throw new InvalidOperationException($"El cliente con ID {request.ClienteId} no existe.");

            // Validar que el evento existe
            var evento = await _eventoRepository.GetAsync(request.EventoId);
            if (evento == null)
                throw new InvalidOperationException($"El evento con ID {request.EventoId} no existe.");

            // Actualizar propiedades básicas
            reserva.SetClienteId(request.ClienteId);
            reserva.SetEventoId(request.EventoId);
            reserva.SetFechaFinalizacion(request.FechaFinalizacion);
            reserva.SetEstado(request.Estado);

            // Limpiar productos actuales
            reserva.LimpiarProductos();

            // Agregar productos actualizados y validar stock
            foreach (var productoRequest in request.Productos)
            {
                var producto = await _productoRepository.GetAsync(productoRequest.ProductoId);
                if (producto == null)
                    throw new InvalidOperationException($"El producto con ID {productoRequest.ProductoId} no existe.");

                if (producto.Stock < productoRequest.CantidadReservada)
                    throw new InvalidOperationException($"Stock insuficiente para el producto '{producto.Nombre}'. Stock disponible: {producto.Stock}, solicitado: {productoRequest.CantidadReservada}");

                reserva.AgregarProducto(productoRequest.ProductoId, productoRequest.CantidadReservada);
            }

            return await _reservaRepository.UpdateAsync(reserva);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _reservaRepository.DeleteAsync(id);
        }

        public async Task<bool> CancelarReservaAsync(int id)
        {
            var reserva = await _reservaRepository.GetAsync(id);
            if (reserva == null) return false;

            reserva.SetEstado("Cancelada");
            return await _reservaRepository.UpdateAsync(reserva);
        }

        public async Task<bool> ConfirmarReservaAsync(int id)
        {
            var reserva = await _reservaRepository.GetAsync(id);
            if (reserva == null) return false;

            // Validar stock antes de confirmar
            foreach (var reservaProducto in reserva.Productos)
            {
                var producto = await _productoRepository.GetAsync(reservaProducto.ProductoId);
                if (producto == null)
                    throw new InvalidOperationException($"El producto con ID {reservaProducto.ProductoId} no existe.");

                if (producto.Stock < reservaProducto.CantidadReservada)
                    throw new InvalidOperationException($"Stock insuficiente para el producto '{producto.Nombre}'. Stock disponible: {producto.Stock}, requerido: {reservaProducto.CantidadReservada}");
            }

            // Confirmar y descontar stock
            reserva.SetEstado("Confirmada");
            
            foreach (var reservaProducto in reserva.Productos)
            {
                var producto = await _productoRepository.GetAsync(reservaProducto.ProductoId);
                if (producto != null)
                {
                    producto.SetStock(producto.Stock - reservaProducto.CantidadReservada);
                    await _productoRepository.UpdateAsync(producto);
                }
            }

            return await _reservaRepository.UpdateAsync(reserva);
        }

        private static ReservaDTO MapToDTO(Reserva reserva)
        {
            return new ReservaDTO
            {
                Id = reserva.Id,
                ClienteId = reserva.ClienteId,
                Cliente = reserva.Cliente != null 
                    ? new ClienteDTO
                    {
                        Id = reserva.Cliente.Id,
                        Nombre = reserva.Cliente.Nombre,
                        Apellido = reserva.Cliente.Apellido,
                        Email = reserva.Cliente.Email,
                        Telefono = reserva.Cliente.Telefono,
                        Direccion = reserva.Cliente.Direccion
                    }
                    : null,
                EventoId = reserva.EventoId,
                Evento = reserva.Evento != null
                    ? new EventoDTO
                    {
                        Id = reserva.Evento.Id,
                        NombreEvento = reserva.Evento.NombreEvento,
                        FechaEvento = reserva.Evento.FechaEvento
                    }
                    : null,
                FechaReserva = reserva.FechaReserva,
                FechaFinalizacion = reserva.FechaFinalizacion,
                Estado = reserva.Estado,
                Productos = reserva.Productos.Select(p => new ReservaProductoDTO
                {
                    ProductoId = p.ProductoId,
                    NombreProducto = p.NombreProducto,
                    PrecioUnitario = p.PrecioUnitario,
                    CantidadReservada = p.CantidadReservada,
                    SubTotal = p.SubTotal
                }).ToList(),
                TotalReserva = reserva.TotalReserva,
                TotalProductos = reserva.TotalProductos,
                CantidadTiposProductos = reserva.CantidadTiposProductos
            };
        }
    }
}
