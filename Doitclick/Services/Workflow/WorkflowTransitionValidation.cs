using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Doitclick.Models.Workflow;

namespace Doitclick.Services.Workflow
{
    public class WorkflowTransitionValidation : IWorkflowTransitionValidation
    {

        
        protected readonly ApplicationDbContext _context;

        public WorkflowTransitionValidation(ApplicationDbContext context, string numeroTicket)
        {
            _context = context;
            Variables = _context.Variables.Where(v => v.NumeroTicket.Equals(numeroTicket));
            NumeroTicket = numeroTicket;
        }

        protected string NumeroTicket { get; set; }

        protected IEnumerable<Variable> Variables { get; }

        protected string Variable(string clave)
        {
            var varu = Variables.FirstOrDefault(v => v.Clave.Equals(clave));
            return varu.Valor;
            
        }

        public virtual bool Validar()
        {
            return true;
        }
    }
}
