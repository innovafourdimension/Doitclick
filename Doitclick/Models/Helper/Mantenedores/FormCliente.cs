using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class FormCliente
    { 
        public int Id {get;set;}
        public string rutCliente { get; set; }
        public string nombreCliente{ get; set; }
        public string tipocliente {get;set;}
        public string esJuridico {get;set;}
        public string prevision{get;set;}
        public string rutFacturacon { get; set; }
        public string razonSocialFacturacion { get; set; }
        public string telefonoFacturacion { get; set; }
        public string direccionFacturacion { get; set; }
        public string giroFacturacion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
    }
}
