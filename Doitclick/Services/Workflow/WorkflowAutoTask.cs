using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Doitclick.Models.Workflow;

namespace Doitclick.Services.Workflow
{
    public class WorkflowAutoTask : IWorkflowAutoTask
    {
        protected readonly ApplicationDbContext _context;
        public WorkflowAutoTask(ApplicationDbContext context, string numeroTicket)
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

        public virtual Task Ejecutar()
        {
            return Task.CompletedTask;
        }
    }
}
