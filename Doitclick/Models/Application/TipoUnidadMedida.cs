using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class TipoUnidadMedida
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activa { get; set; }
        public IEnumerable<MaterialMensual> MaterialMensual {get;set;}
        public IEnumerable<MaterialDisponible> MAterialDisponible { get; set; }
    }
}
