using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Helper
{
    public class ViewHelper
    {
        public static string EstadosInstrumentos(string marca)
        {
            string estado = "Sin Valor";
            switch (marca)
            {
                case "BN":
                    estado = "Nuevo";
                    break;                
                case "RG":
                    estado = "Regular";
                    break;
                case "ML":
                    estado = "Malo";
                    break;
                case "BJ":
                    estado = "De Baja";
                    break;
            } 

            return estado;   
        }
    }
     
    
}
