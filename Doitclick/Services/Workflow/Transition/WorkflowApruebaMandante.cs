using System;
using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowApruebaMandante : WorkflowTransitionValidation
    {
        public WorkflowApruebaMandante(ApplicationDbContext context, string numeroTicket) : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;
            string aprobado = "0";
            if(Variable("TRABAJO_CON_REPAROS_MANDANTE") == aprobado)
            {
                retorno = true;
            }

            return retorno;
        }
    }
}