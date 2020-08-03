using System;
using System.Collections.Generic;

namespace FurmularioDonaciones.Models
{
    public partial class FormularioDonacion
    {
        public decimal IdFormulario { get; set; }
        public int IdUsuario { get; set; }
        public int? IdMarca { get; set; }
        public string CiDonante { get; set; }
        public string NombreDonante { get; set; }
        public string ApellidoDonante { get; set; }
        public string LoteProductoDonancion { get; set; }
        public DateTime FechaVencimientoProducto { get; set; }
        public DateTime FechaAcopioDonacion { get; set; }
        public byte[] Foto { get; set; }
        public string Tipoimagen { get; set; }

        public virtual Marcas IdMarcaNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
