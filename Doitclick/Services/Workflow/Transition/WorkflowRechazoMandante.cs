using Doitclick.Data;

namespace Doitclick.Services.Workflow.Transition
{
    public class WorkflowRechazoMandante : WorkflowTransitionValidation
    {
        public WorkflowRechazoMandante(ApplicationDbContext context, string numeroTicket) : base(context, numeroTicket)
        {
        }

        public override bool Validar()
        {
            bool retorno = false;
            string reprobado = "1";
            if(Variable("TRABAJO_CON_REPAROS_MANDANTE") == reprobado)
            {
                retorno = true;
            }

            return retorno;
        }
    }
}