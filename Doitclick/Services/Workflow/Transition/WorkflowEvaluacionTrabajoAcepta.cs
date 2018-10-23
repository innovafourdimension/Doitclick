using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowEvaluacionTrabajoAcepta : WorkflowTransitionValidation
    {
        public WorkflowEvaluacionTrabajoAcepta(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("LABORATORISTA_ACEPTA_TRABAJO") == "1")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
