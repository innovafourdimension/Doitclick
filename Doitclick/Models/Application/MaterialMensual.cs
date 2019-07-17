using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public class MaterialMensual
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public TipoUnidadMedida UnidadMedida { get; set; }       
        public int StockAlerta { get;set; }
        public int Cantidad {get;set;}
        public string Codigo { get; set; }
        public Marca Marca { get; set; }
        public bool Activa { get; set; }
        public int stockActual { get; set; }


        public void AgregarStock(int stockAgregar)
        {

            if(stockAgregar > 0){
                this.stockActual = this.stockActual + stockAgregar;
            }else{
                throw new Exception("Debes agregar un stock > 0");
            }
        }

        public void RestarStock(int stockRestar)
        {
            if((this.stockActual - stockRestar) < 0)
            {
                throw new Exception("El stock resultante es negativo. No hay stock");
            }

            this.stockActual = this.stockActual - stockRestar;
        }
    }
}
