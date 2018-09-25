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
                case "N":
                    estado = "Nuevo";
                    break;                
                case "R":
                    estado = "Regular";
                    break;
                case "M":
                    estado = "Malo";
                    break;
                case "B":
                    estado = "De Baja";
                    break;
            } 

            return estado;   
        }
    }
     
    
}
