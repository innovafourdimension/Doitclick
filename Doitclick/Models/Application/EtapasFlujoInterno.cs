using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Application
{
    public static class EtapasFlujoInterno
    {
        public const string InicioProceso = "INICIO_PROCESO";
        public const string IngresoDatosPaciente = "INGRESO_DATOS_PACIENTE";
        public const string AsignarTrabajo = "ASIGNAR_TRABAJO";
        public const string AsignarCotizacion = "ASIGNAR_COTIZACION";
        public const string EvaluarTrabajo = "EVALUA_TRABAJO";
        public const string EvaluarCotizacion = "EVALUA_COTIZACION";
        public const string CobroServicio = "COBRO_SERVICIO";
        public const string InformeRechazo = "INFORME_DE_RECHAZO";
        public const string EjecutarTrabajo = "EJECUCION_DE_TRABAJO";
        public const string ControlCalidad = "CONTROL_DE_CALIDAD";
        public const string EntregaServicio = "ENTREGA_SERVICIO";
        public const string ValidacionMandante = "VALIDACION_MANDANTE";
    }
}
