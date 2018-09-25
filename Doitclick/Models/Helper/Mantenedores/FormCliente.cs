using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class FormCliente
    { 
        public int Id {get;set;}
        public string RutCliente { get; set; }
        public string NombreCliente{ get; set; }
        public string tipocliente {get;set;}
        public int optradio {get;set;}
        public int prevision{get;set;}
    }
}
