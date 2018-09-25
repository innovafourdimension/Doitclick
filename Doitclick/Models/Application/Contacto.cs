using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class Contacto
    {
        public int Id { get; set; }
        public TipoContacto TipoContacto { get; set; }
        public Cliente Cliente { get; set; }
        public string Resumen { get; set; }
        public bool EsPrincipal { get; set; }
        public ICollection<MetaDatosContacto> MetaDatosContacto { get; set; }
    }
}
