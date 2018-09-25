using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Workflow;

namespace Doitclick.Models.Application
{
    public class MovimientoCuentaCorriente
    {
        public int Id { get; set; }
        public TipoTransaccionCuentaCorriente TipoTransanccion { get; set; }
        public CuentaCorriente CuentaCorriente { get; set; }
        public string NumeroTransaccion { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public long MontoTransaccion { get; set; }
        public string Resumen { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroTicket { get; set; }
    }
}
