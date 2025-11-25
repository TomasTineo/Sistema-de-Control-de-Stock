using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domain.Model
{
    public class Evento
    {
        public int Id { get; private set; }
        public string NombreEvento { get; private set; }
        public DateTime FechaEvento { get; private set; }

        public Evento() { }
        
        public Evento(int id, string nombreEvento, DateTime fechaEvento)
        {
            SetId(id);
            SetNombre(nombreEvento);
            SetFecha(fechaEvento);
        }

        
        public Evento(string nombreEvento, DateTime fechaEvento)
        {
            SetNombre(nombreEvento);
            SetFecha(fechaEvento);
        }

        public void SetId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser un número positivo.", nameof(id));
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            NombreEvento = nombre;
        }

        public void SetFecha(DateTime fecha)
        {
            var ahora = DateTime.Now; // tomamos la fecha una sola vez para no generar condición de carrera
            
            if (fecha == default(DateTime))
                throw new ArgumentException("La fecha del evento no puede estar vacía.", nameof(fecha));
            
            // No permite fechas de mas de 2 años
            var fechaMaxima = ahora.AddYears(2);
            if (fecha > fechaMaxima)
                throw new ArgumentException("La fecha del evento no puede ser tan lejana en el futuro.", nameof(fecha));
            
            // No permite fechas del pasado en la carga de eventos
            if (Id == 0 && fecha.Date < ahora.Date)
                throw new ArgumentException("La fecha del evento no puede ser en el pasado.", nameof(fecha));
            
            FechaEvento = fecha;
        }
    }
}
