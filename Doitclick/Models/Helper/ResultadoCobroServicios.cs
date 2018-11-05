using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper
{
    public class ResultadoCobroServicios
    {
        public string NumeroTicket { get; set; }
        public string NumeroDocumento { get; set; }
        public string FormaPago { get; set; }
        public int ValorPagar { get; set; }
        public int SaldoPendiente { get; set; }
        public int ValorTotal { get; set; }
        public int AceptaPresupuesto { get; set; }

    }
}