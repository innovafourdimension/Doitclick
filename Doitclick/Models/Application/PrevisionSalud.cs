using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class PrevisionSalud
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activa { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
    }
}
