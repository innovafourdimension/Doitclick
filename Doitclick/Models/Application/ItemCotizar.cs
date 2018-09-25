using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class ItemCotizar
    {
        public int Id { get; set; }
        public Cotizacion Cotizacion { get; set; }
        public Servicio Servicio { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }

        /*
         Si el Servicio es null quiere decir que es un trabajo especial.
         */
    }
}
