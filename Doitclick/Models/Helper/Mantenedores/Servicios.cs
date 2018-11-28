
using System;
using System.Collections.Generic;

namespace Doitclick.Models.Helper.Mantenedores
{
    public class ServicioFormularioIngreso
    {
        public int Id   {get;set;}
        public string NombreServicio     {get;set;}
        public string DescripcionServicio{get;set;}
        public string CodigoServicio     {get;set;}
        public int VManoObra          {get;set;}
        public int PorcentajeComision {get;set;}
        public int ValorCosto { get; set; }
        public IEnumerable<MaterialFormItem> Materiales { get; set; }
    }

    public class MaterialFormItem{
        public int MaterialId { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public int ValorTotal { get; set; }
    }
}


