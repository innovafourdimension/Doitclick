using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowNoSePuedeEjecutar : WorkflowTransitionValidation
    {
        public WorkflowNoSePuedeEjecutar(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;
            if(Variable("SE_PUEDE_EJECUTAR") == "0")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
