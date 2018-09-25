using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombres { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public bool EsPersonalidadJuridica { get; set; }
        public PrevisionSalud PrevisionSalud { get; set; }
        public ICollection<MetaDatosCliente> MetaDatosCliente { get; set; }
        public ICollection<Cotizacion> Cotizaciones { get; set; }
    }
}
