using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowTrabajoConReparos : WorkflowTransitionValidation
    {
        public WorkflowTrabajoConReparos(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("TRABAJO_CON_REPAROS_CC") == "1")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
