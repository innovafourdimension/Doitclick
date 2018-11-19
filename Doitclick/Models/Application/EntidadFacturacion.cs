using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Doitclick.Models.Application
{
    public class EntidadFacturacion
    {
        public Guid Id { get; set; }
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Giro { get; set; }
    }
}