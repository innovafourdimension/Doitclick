using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Workflow;
using Doitclick.Services.Workflow;

namespace Doitclick.Services
{
    public class WorkflowService : IWorkflowService
    {

        private readonly IWorkflowKernel _kernel;
        public WorkflowService(IWorkflowKernel kernel)
        {
            _kernel = kernel;
        }

        public void Abortar()
        {
            throw new NotImplementedException();
        }

        public void Avanzar(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario)
        {
            _kernel.CompletarTarea(nombreInternoProceso, nombreInternoEtapa, numeroTicket, identificacionUsuario);
        }

        public Solicitud Instanciar(string nombreProceso, string identificacionUsuario, string resumenInstancia)
        {
            return _kernel.GenerarSolicitud(nombreProceso, identificacionUsuario, resumenInstancia);
        }

        public void AsignarVariable(string clave, string valor, string numeroTicket)
        {
            _kernel.SetVariable(clave, valor, numeroTicket);
        }

        public string ObtenerVariable(string clave, string numeroTicket)
        {
            return _kernel.GetVariableValue(clave, numeroTicket);
        }
    }
}
