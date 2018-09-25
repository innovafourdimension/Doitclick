using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class FormRol
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public int Organizacion { get; set; }
        public bool Comisionista { get; set; }
    }
}
