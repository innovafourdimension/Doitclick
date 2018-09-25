using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class FormInstrumento
    {
        public int Id { get; set; }
        public string NombreInstrumento { get; set; }
        public string CodigoInstrumento { get; set; }
        public int Marca { get; set; }
        public string Estado { get; set; }
        public string Descripcion { get; set; }
    }
}
