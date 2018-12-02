using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowEsVirtual : WorkflowTransitionValidation
    {
        public WorkflowEsVirtual(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;
            if(Variable("TIPO_TRABAJO") == "VIRTUAL")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
