using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MaterialMensualSolicitado
    {
        public string Id { get; set; }
        public MaterialMensual MaterialMensual { get; set; }
        public SolicitudMaterialMensual SolicitudMaterialMensual { get; set; }
    }
}

