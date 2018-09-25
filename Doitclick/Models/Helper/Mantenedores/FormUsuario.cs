using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class FormUsuario
    {
        public string Nombres { get; set; }
        public string Identificacion { get; set; }
        public string Correo { get; set; }
        public string Llave { get; set; }
        public string TipoUsuario { get; set; }
        public string PorcentajeComision { get; set; }
        public string Rol {get; set;}
        public string Telefono { get; set; }
    }
}
