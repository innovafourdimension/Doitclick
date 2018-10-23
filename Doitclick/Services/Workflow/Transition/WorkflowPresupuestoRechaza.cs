using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowPresupuestoRechaza : WorkflowTransitionValidation
    {
        public WorkflowPresupuestoRechaza(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("CLIENTE_ACEPTA_PRESUPUESTO") == "0")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
