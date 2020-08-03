using System;
using System.Collections.Generic;

namespace FurmularioDonaciones.Models
{
    public partial class Marcas
    {
        public Marcas()
        {
            FormularioDonacion = new HashSet<FormularioDonacion>();
        }

        public int IdMarca { get; set; }
        public string NombreMarca { get; set; }

        public virtual ICollection<FormularioDonacion> FormularioDonacion { get; set; }
    }
}
