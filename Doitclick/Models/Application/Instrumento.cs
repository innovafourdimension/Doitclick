using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class Instrumento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public Marca Marca { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public bool Activa { get; set; }
        
    }
}
