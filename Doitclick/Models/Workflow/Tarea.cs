﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doitclick.Models.Workflow
{
    public class Tarea
    {
        public int Id { get; set; }
        public Solicitud Solicitud { get; set; }
        public Etapa Etapa { get; set; }
        public string AsignadoA { get; set; }
        public string ReasignadoA { get; set; }
        public string EjecutadoPor { get; set; }
        public EstadoTarea Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaTerminoEstimada { get; set; }
        public DateTime? FechaTerminoFinal { get; set; }
    }
}