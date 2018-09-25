using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class CuentaCorriente
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<MovimientoCuentaCorriente> Movimientos { get; set; }
    }
}
