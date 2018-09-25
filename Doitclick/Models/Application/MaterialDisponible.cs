using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MaterialDisponible
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public TipoUnidadMedida UnidadMedida { get; set; }
        public int PrecioUnidad { get; set; }
        public int StockAlerta { get; set; }
        public string Codigo { get; set; }
        public Marca Marca { get; set; }
        public bool Activa { get; set; }
        public ICollection<MovimientoMaterialDisponoble> Movimientos { get; set; }
        public ICollection<MaterialPresupuestado> Presupuestado { get; set; }
    }
}