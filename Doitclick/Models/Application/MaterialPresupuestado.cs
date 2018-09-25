using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MaterialPresupuestado
    {
        public int Id { get; set; }
        public Servicio Servicio { get; set; }
        public MaterialDisponible MaterialDisponible { get; set; }
        public int CantidadMaterial { get; set; }
    }
}
