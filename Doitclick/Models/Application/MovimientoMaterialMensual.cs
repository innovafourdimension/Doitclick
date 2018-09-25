using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MovimientoMaterialMensual
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string RutSolicitante {get;set;}
        public DateTime FechaSolicitud {get;set;}
        public string Estado{get;set;}
        public DateTime FechaEstado {get;set;}
        public MaterialMensual Material {get;set;}
        public int Cantidad {get;set;}

    }
}
