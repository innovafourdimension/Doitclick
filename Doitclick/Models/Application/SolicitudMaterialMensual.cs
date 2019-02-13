using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class SolicitudMaterialMensual
    {
        public string Id { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Comentarios { get; set; }
        public string Estado { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string ComentariosFin { get; set; }
    }
}
