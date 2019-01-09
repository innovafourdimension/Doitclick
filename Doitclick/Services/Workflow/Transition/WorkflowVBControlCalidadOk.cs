using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowVBControlCalidadOk : WorkflowTransitionValidation
    {
        public WorkflowVBControlCalidadOk(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;
            if(Variable("CONTROL_CALIDAD_OK") == "1")
            {
                retorno = true;
            }
            return retorno;
        }
    }
}
