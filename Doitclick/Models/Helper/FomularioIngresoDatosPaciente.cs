using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper
{
    public class FomularioIngresoDatosPaciente
    { 
        public string RutPaciente { get; set; }
        public string NombrePaciente{ get; set; }
        public string ApellidosPaciente{ get; set; }
        public string Telefono{ get; set; }
        public string Correo{ get; set; }
        public string DrSolicitante{ get; set; }
        public string NroOrden{ get; set; }
        public int PrevisionSalud { get; set; }
        public string SrcImagen { get; set; }
        public string CotizacionEspecial { get; set; }
        public IEnumerable<ServicioItemFormulario> Servicios { get; set; }
    }
}
