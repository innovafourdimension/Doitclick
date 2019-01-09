using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Application;
using Doitclick.Models.Security;

namespace Doitclick.Models.Helper
{
    public class CotizacionViewModel
    {
        public IEnumerable<ItemCotizar> Servicios  { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public Usuario DrMandante { get; set; }
        public IEnumerable<MovimientoCuentaCorriente> Movimientos { get; set; }
        public CalculoBalance CalculoBalance { get; set; }
    }

    public class CalculoBalance{
        public float Cargos { get; set; }
        public float Pagos { get; set; }
        public float Balance { 
            get{
                return Cargos - Pagos; 
            } 
        }
    }
}
