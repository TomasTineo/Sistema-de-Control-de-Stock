using DTOs;
using Domain.Model;
using Data.Repositories;

namespace Application.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<EventoDTO?> GetAsync(int id)
        {
            var evento = await _eventoRepository.GetAsync(id);
            
            if (evento == null) return null;

            return new EventoDTO
            {
                Id = evento.Id,
                NombreEvento = evento.NombreEvento,
                FechaEvento = evento.FechaEvento
            };
        }

        public async Task<IEnumerable<EventoDTO>> GetAllAsync()
        {
            var eventos = await _eventoRepository.GetAllAsync();
            
            return eventos.Select(e => new EventoDTO
            {
                Id = e.Id,
                NombreEvento = e.NombreEvento,
                FechaEvento = e.FechaEvento
            });
        }

        public async Task<EventoDTO> CreateAsync(CreateEventoRequest request)
        {
            // Constructor SIN ID - EF asignará automáticamente
            var evento = new Evento(request.NombreEvento, request.FechaEvento);
            
            var eventoCreado = await _eventoRepository.AddAsync(evento);

            return new EventoDTO
            {
                Id = eventoCreado.Id,
                NombreEvento = eventoCreado.NombreEvento,
                FechaEvento = eventoCreado.FechaEvento
            };
        }

        public async Task<bool> UpdateAsync(UpdateEventoRequest request)
        {
            var evento = await _eventoRepository.GetAsync(request.Id);
            if (evento == null) return false;

            // Actualizar propiedades
            evento.SetNombre(request.NombreEvento);
            evento.SetFecha(request.FechaEvento);

            return await _eventoRepository.UpdateAsync(evento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _eventoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EventoDTO>> GetByFechaRangeAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            var eventos = await _eventoRepository.GetByFechaRangeAsync(fechaInicio, fechaFin);
            
            return eventos.Select(e => new EventoDTO
            {
                Id = e.Id,
                NombreEvento = e.NombreEvento,
                FechaEvento = e.FechaEvento
            });
        }
    }
}