using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MetaDatosCliente
    {
        public int Id { get; set; }
        public Cliente Cliente { get; set; }
        public string Clave { get; set; }
        public string Valor { get; set; }
        public int Orden { get; set; }
    }
}
