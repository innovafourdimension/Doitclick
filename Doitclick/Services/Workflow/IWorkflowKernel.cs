using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Workflow;
using Doitclick.Models.Security;

namespace Doitclick.Services.Workflow
{
    public interface IWorkflowKernel
    {
        Solicitud GenerarSolicitud(Proceso proceso, Usuario usuario);

        Solicitud GenerarSolicitud(string nombreInternoProceso, string identificacionUsuario, string resumen);

        void ActivarTarea(Proceso proceso, Etapa etapa, Usuario usuario);

        void ActivarTarea(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario);

        void ActivarTareaAsync(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario);

        void CompletarTarea(Tarea tarea, Usuario usuario);

        void CompletarTarea(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario);

        void AbortarSolicitud(Solicitud solicitud);

        void AbortarSolicitud(string NumeroTicket);

        Task<int> AbortarSolicitudAsync(string NumeroTicket);

        void SetVariable(string key, string value, string numeroTicket);

        string GetVariableValue(string key, string numeroTicket);
    }
}
