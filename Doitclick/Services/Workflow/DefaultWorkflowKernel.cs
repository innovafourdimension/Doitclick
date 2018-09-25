using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Models.Security;
using Doitclick.Models.Workflow;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace Doitclick.Services.Workflow
{
    public class DefaultWorkflowKernel : IWorkflowKernel
    {

        private readonly ApplicationDbContext _context;
        
        public DefaultWorkflowKernel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AbortarSolicitud(Solicitud solicitud)
        {
            throw new NotImplementedException();
        }

        public void AbortarSolicitud(string NumeroTicket)
        {
            DateTime momentoCierre = DateTime.Now;
            
            Solicitud solicitud  = _context.Solicitudes.FirstOrDefault(s => s.NumeroTicket == NumeroTicket);
            solicitud.FechaTermino = momentoCierre;
            solicitud.Estado = EstadoSolicitud.Abortada;

            Tarea tarea = _context.Tareas.FirstOrDefault(x => x.Solicitud.NumeroTicket == NumeroTicket && x.Estado == EstadoTarea.Iniciada && x.FechaTerminoFinal == null);
            tarea.FechaTerminoFinal = momentoCierre;
            tarea.Estado = EstadoTarea.Abortada;

            _context.Entry(solicitud).State = EntityState.Modified;
            _context.Entry(tarea).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public async Task<int> AbortarSolicitudAsync(string NumeroTicket)
        {
            DateTime momentoCierre = DateTime.Now;

            Solicitud solicitud = _context.Solicitudes.FirstOrDefault(s => s.NumeroTicket == NumeroTicket);
            solicitud.FechaTermino = momentoCierre;
            solicitud.Estado = EstadoSolicitud.Abortada;

            Tarea tarea = _context.Tareas.FirstOrDefault(x => x.Solicitud.NumeroTicket == NumeroTicket && x.Estado == EstadoTarea.Iniciada && x.FechaTerminoFinal == null);
            tarea.FechaTerminoFinal = momentoCierre;
            tarea.Estado = EstadoTarea.Abortada;

            _context.Entry(solicitud).State = EntityState.Modified;
            _context.Entry(tarea).State = EntityState.Modified;

            return await _context.SaveChangesAsync();
            
        }

        public void ActivarTarea(Proceso proceso, Etapa etapa, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void ActivarTarea(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario)
        {
            //Primero obtengo el proceso
            Proceso proceso = _context.Procesos.Include(d => d.Etapas).FirstOrDefault(x => x.NombreInterno == nombreInternoProceso);

            //Segundo Obtengo la etapa
            Etapa etapa = proceso.Etapas.FirstOrDefault(x => x.NombreInterno == nombreInternoEtapa);
            
            //Tercero obtengo la solicitud
            Solicitud solicitud = proceso.Solicitudes.FirstOrDefault(d => d.NumeroTicket == numeroTicket);

            Tarea tarea = new Tarea
            {
                Etapa = etapa,
                Estado = EstadoTarea.Activada,
                FechaInicio = DateTime.Now,
                Solicitud = solicitud,
                AsignadoA = etapa.ValorUsuarioAsignado
            };
            
            _context.Tareas.Add(tarea);
            _context.SaveChanges();
            var lst = _context.TareasAutomaticas.Include(x => x.Etapa).Where(x => x.Etapa.Id == etapa.Id && x.EventoDisparador == TipoEventoEtapa.AlActivar).ToList();
            foreach (var x in lst)
            {
                EjecutaTrabajo(x.Namespace, x.Clase, x.Metodo, numeroTicket);
            }

            if(etapa.TipoUsuarioAsignado == TipoUsuarioAsignado.Boot)
            {
                CompletarTarea(nombreInternoProceso, nombreInternoEtapa, numeroTicket, identificacionUsuario);
            }
        }

        public async void ActivarTareaAsync(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario)
        {
            //Primero obtengo el proceso
            Proceso proceso = _context.Procesos.FirstOrDefault(x => x.NombreInterno == nombreInternoProceso);

            //Segundo Obtengo la etapa
            Etapa etapa = proceso.Etapas.FirstOrDefault(x => x.NombreInterno == nombreInternoEtapa);

            //Tercero obtengo la solicitud
            Solicitud solicitud = proceso.Solicitudes.FirstOrDefault(d => d.NumeroTicket == numeroTicket);

            Tarea tarea = new Tarea
            {
                Etapa = etapa,
                Estado = EstadoTarea.Activada,
                FechaInicio = DateTime.Now,
                Solicitud = solicitud,
                AsignadoA = etapa.ValorUsuarioAsignado
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
        }

        public void CompletarTarea(Tarea tarea, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void CompletarTarea(string nombreInternoProceso, string nombreInternoEtapa, string numeroTicket, string identificacionUsuario)
        {
            Proceso proceso = _context.Procesos.FirstOrDefault(p => p.NombreInterno == nombreInternoProceso);
            Solicitud solicitud = _context.Solicitudes.Include(x=>x.Tareas).FirstOrDefault(c => c.NumeroTicket.Equals(numeroTicket));
            Tarea tareaActual = _context.Tareas.Include(t => t.Solicitud).Include(t => t.Etapa).FirstOrDefault(d => d.Etapa.NombreInterno.Equals(nombreInternoEtapa) && d.FechaTerminoFinal == null && d.Estado == EstadoTarea.Activada && d.Solicitud.NumeroTicket == numeroTicket);

            tareaActual.EjecutadoPor = identificacionUsuario;
            tareaActual.FechaTerminoFinal = DateTime.Now;
            tareaActual.Estado = EstadoTarea.Finalizada;

            _context.Entry(tareaActual).State = EntityState.Modified;
            _context.SaveChanges();
            
            ICollection<Transito> transiciones = _context.Transiciones.Include(d => d.EtapaActaual).Include(d => d.EtapaDestino).Where(d => d.EtapaActaual.NombreInterno == nombreInternoEtapa).ToList();
            foreach (Transito transicion in transiciones)
            {
                bool estadoAvance = EjecutaValidacion(transicion.NamespaceValidacion, transicion.ClaseValidacion, transicion.MetodoValidacion, numeroTicket);
                if(estadoAvance)
                {
                    ActivarTarea(nombreInternoProceso, transicion.EtapaDestino.NombreInterno, numeroTicket, identificacionUsuario);
                }
            }

        }

        public Solicitud GenerarSolicitud(Proceso proceso, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Solicitud GenerarSolicitud(string nombreInternoProceso, string identificacionUsuario, string resumen)
        {
            Proceso proceso = _context.Procesos.Include(e => e.Etapas).FirstOrDefault(p => p.NombreInterno == nombreInternoProceso);
            string ticket = GeneraTicket(proceso.Id.ToString());
            Solicitud solicitud = new Solicitud
            {
                FechaInicio = DateTime.Now,
                Estado = EstadoSolicitud.Iniciada,
                InstanciadoPor = identificacionUsuario,
                NumeroTicket = ticket,
                Proceso = proceso,
                Resumen = resumen
            };

            _context.Solicitudes.Add(solicitud);
            _context.SaveChanges();

            var etapaInicial = proceso.Etapas.FirstOrDefault(x => x.TipoEtapa == TipoEtapa.Inicio && x.Secuencia == proceso.Etapas.Min(d => d.Secuencia));
            if (etapaInicial.TipoUsuarioAsignado == TipoUsuarioAsignado.Boot)
                identificacionUsuario = "wfboot";

            ActivarTarea(nombreInternoProceso, etapaInicial.NombreInterno, ticket, identificacionUsuario);

            return solicitud;
        }

        private string GeneraTicket(string IdProceso)
        {
            DateTime now = DateTime.Now;
            return now.Year.ToString() + now.Month.ToString().PadLeft(2, '0') + now.Day.ToString().PadLeft(2, '0') + IdProceso.PadLeft(2, '0') + (now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString() + now.Millisecond.ToString()).PadLeft(10, '0');
        }

        private bool EjecutaValidacion(string elnamespace, string laclase, string elmetodo, string numeroTicket)
        {

            object[] losparametros =
            {
                _context,
                numeroTicket
            };
            Type type = typeof(IWorkflowTransitionValidation);
            MethodInfo method = type.GetMethod(elmetodo);
            Type implementacion = Type.GetType(elnamespace + "." + laclase);
            IWorkflowTransitionValidation instancia = (IWorkflowTransitionValidation)Activator.CreateInstance(implementacion, losparametros);
            return (bool)method.Invoke(instancia, null);
        }

        private bool EjecutaTrabajo(string elnamespace, string laclase, string elmetodo, string numeroTicket)
        {

            object[] losparametros =
            {
                _context,
                numeroTicket
            };
            Type type = typeof(IWorkflowAutoTask);
            MethodInfo method = type.GetMethod(elmetodo);
            Type implementacion = Type.GetType(elnamespace + "." + laclase);
            IWorkflowAutoTask instancia = (IWorkflowAutoTask)Activator.CreateInstance(implementacion, losparametros);
            return (bool)method.Invoke(instancia, null);
        }

        public void SetVariable(string key, string value, string numeroTicket)
        {
            if(_context.Variables.Any(s=>s.Clave == key && s.NumeroTicket == numeroTicket))
            {
                Variable variable = _context.Variables.FirstOrDefault(s => s.Clave == key && s.NumeroTicket == numeroTicket);
                variable.Valor = value;
                _context.Entry(variable).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                Variable variable = new Variable
                {
                    Clave = key,
                    Valor = value,
                    NumeroTicket = numeroTicket,
                    Tipo = "string"
                };
                _context.Variables.Add(variable);
                _context.SaveChanges();
            }
        }

        public string GetVariableValue(string key, string numeroTicket)
        {
            Variable variable = _context.Variables.FirstOrDefault(d => d.Clave == key && d.NumeroTicket == numeroTicket);
            return variable.Valor;
        }
    }
}
