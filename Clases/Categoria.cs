using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Categoria
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; } = string.Empty;
        
        public virtual ICollection<Producto> Productos { get; private set; } = new List<Producto>();

        protected Categoria() { }

        public Categoria(int id, string nombre)
        {
            SetId(id);
            SetNombre(nombre);
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
            Nombre = nombre;
        }

    }
}
