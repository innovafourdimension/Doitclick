using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowEvaluacionCotizacionRechaza : WorkflowTransitionValidation
    {
        public WorkflowEvaluacionCotizacionRechaza(ApplicationDbContext context, string numeroTicket)
                : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;

            if (Variable("LABORATORISTA_ACEPTA_COTIZACION") == "0")
            {
                retorno = true;
            }

            return retorno;
        }
    }
}
