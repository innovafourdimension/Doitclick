using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class Servicio
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Resumen { get; set; }
        public string Codigo { get; set; }
        public int ValorManoObra { get; set; }
        public int ValorMateriales { get; set; }
        public int ValorComision { get; set; }
        public int PorcentajeComision { get; set; }
        public int ValorTotal { get; set; }
        public bool Activa { get; set; }

        public ICollection<ItemCotizar> ServiciosCotizar { get; set; }
        public ICollection<MaterialPresupuestado> MaterialesPresupuestados { get; set; }

    }
}
