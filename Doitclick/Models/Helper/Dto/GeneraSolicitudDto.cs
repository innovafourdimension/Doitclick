using System.Collections.Generic;

namespace Doitclick.Models.Helper.Dto
{
    public class GeneraSolicitudContainerDto
    {
        public string comentarios { get; set; }
        public IEnumerable<MaterialSolicitadoDto> materiales { get; set; }
    }

    public class MaterialSolicitadoDto
    {
        public int id { get; set; }
        public int cantidad { get; set; }
    }

    public class ProcesaSolicitudDto
    {
        public string tipoResultado { get; set; }
        public string comentarios { get; set; }
        public string idSolicitud { get; set; }
    }

    public class AgregarStockMaterialMensualDto
    {
        public int id { get; set; }
        public int cantidad { get; set; }
    }

}