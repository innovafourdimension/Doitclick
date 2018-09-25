using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MetaDatosContacto
    {
        public int Id { get; set; }
        public Contacto Contacto { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public int Orden { get; set; }
    }
}
