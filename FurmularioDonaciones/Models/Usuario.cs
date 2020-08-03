using System;
using System.Collections.Generic;

namespace FurmularioDonaciones.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            FormularioDonacion = new HashSet<FormularioDonacion>();
        }

        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public string EMailUsuario { get; set; }
        public string PasswordUsuario { get; set; }
        public DateTime? FechaCreacionUsuario { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual ICollection<FormularioDonacion> FormularioDonacion { get; set; }
    }
}
