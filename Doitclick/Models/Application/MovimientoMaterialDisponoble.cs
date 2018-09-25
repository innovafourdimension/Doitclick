using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MovimientoMaterialDisponoble
    {
        public int Id { get; set; }
        public string NumeroTransaccion { get; set; }
        public TipoTransaccionMaterialDisponible TipoTransaccion { get; set; }
        public MaterialDisponible MaterialDisponible { get; set; }
        public DateTime FechaTransaccion { get; set; }
        public string Resumen { get; set; }
        public int CantidadMaterial { get; set; }
        public string NumeroTicket { get; set; }
    }
}
