using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Application;
using Doitclick.Models.Security;
using Doitclick.Models.Workflow;


namespace Doitclick.Models.Helper
{
    public class ListadoInicioContainer
    {
        public Tarea Tarea { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public CotizacionExterno CotizacionExterno { get; set; }
        public Usuario Mandante { get; set; }
    }


    public class HistorialCerradasContainer
    {
        public Solicitud Solicitud { get; set; }
        public Cotizacion Cotizacion { get; set; }
        //public IEnumerable<Variable> Variables { get; set; }

        public Variable Variable { get; set; }
    }
}
