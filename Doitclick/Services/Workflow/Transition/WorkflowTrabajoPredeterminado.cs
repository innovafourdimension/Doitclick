using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowTrabajoPredeterminado : WorkflowTransitionValidation
    {
        public WorkflowTrabajoPredeterminado(ApplicationDbContext context, string numeroTicket) 
                :base(context, numeroTicket)
        {
        }
        
        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("ES_TRABAJO_PREDETERMINADO").Equals("1"))
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
