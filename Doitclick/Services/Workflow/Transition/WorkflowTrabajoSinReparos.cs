using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowTrabajoSinReparos : WorkflowTransitionValidation
    {
        public WorkflowTrabajoSinReparos(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("TRABAJO_CON_REPAROS_CC") == "0")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
