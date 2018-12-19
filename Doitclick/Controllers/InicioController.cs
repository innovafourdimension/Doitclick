using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Doitclick.Models.Helper;
using Doitclick.Models.Workflow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    [Authorize]
    [Route("mi-gestion")]
    public class InicioController : Controller
    {
        private readonly ApplicationDbContext _context;
        public InicioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("bandeja-tareas/internos")]
        [HttpGet]
        public IActionResult BandejaTareasInternos(int limit = 10, int offset = 0, string search = "")
        {

            var rut = User.Identity.Name;
            var bandeja = from tarea in _context.Tareas
                          .Include(t => t.Solicitud).ThenInclude(s => s.Proceso)
                          .Include(t => t.Etapa)
                          join cotiza in _context.Cotizaciones
                          .Include(x=>x.Cliente) on tarea.Solicitud.NumeroTicket equals cotiza.NumeroTicket
                          where tarea.Solicitud.Proceso.Id == 1 && tarea.Estado == EstadoTarea.Activada && (tarea.AsignadoA == rut || (tarea.Etapa.TipoUsuarioAsignado == TipoUsuarioAsignado.Rol && User.IsInRole(tarea.AsignadoA)))
                          select new ListadoInicioContainer { Tarea = tarea, Cotizacion = cotiza };
                          
            

            /* var bandeja2 = from tarea in _context.Tareas
                        .Include(t => t.Solicitud).ThenInclude(s => s.Proceso)
                        .Include(t => t.Etapa)
                        join cotiza in _context.CotizacionesExternos on tarea.Solicitud.NumeroTicket equals cotiza.NumeroTicket  
                        where tarea.Solicitud.Proceso.Id == 1 && tarea.Estado == EstadoTarea.Activada && (tarea.AsignadoA == rut || (tarea.Etapa.TipoUsuarioAsignado == TipoUsuarioAsignado.Rol && User.IsInRole(tarea.AsignadoA)))
                        select new ListadoInicioContainer { Tarea = tarea, CotizacionExterno = cotiza };    
            

            var bandejaFinal = bandeja.Union(bandeja2);*/


            if (!string.IsNullOrEmpty(search))
            {
                bandeja = bandeja.Where(x => x.Tarea.Solicitud.NumeroTicket.Contains(search) || x.Cotizacion.Cliente.Rut.Contains(search) || x.Cotizacion.Cliente.Nombres.Contains(search));
            }

            BootstrapTableResult<ListadoInicioContainer> salida = new BootstrapTableResult<ListadoInicioContainer>();
            salida.total = bandeja.Count();
            salida.rows = bandeja.Skip(offset).Take(limit).ToList();
            
            return Ok(salida);
        }

        [Route("bandeja-tareas/externos")]
        [HttpGet]
        public IActionResult BandejaTareasExternos(int limit = 10, int offset = 0, string search = "")
        {

            var rut = User.Identity.Name;
            var bandeja = from tarea in _context.Tareas
                        .Include(t => t.Solicitud).ThenInclude(s => s.Proceso)
                        .Include(t => t.Etapa)
                        join cotiza in _context.CotizacionesExternos on tarea.Solicitud.NumeroTicket equals cotiza.NumeroTicket  
                        join usr in _context.Users on cotiza.EntidadSolicitante equals usr.Identificador 
                        where tarea.Solicitud.Proceso.Id == 2 && tarea.Estado == EstadoTarea.Activada && (tarea.AsignadoA == rut || (tarea.Etapa.TipoUsuarioAsignado == TipoUsuarioAsignado.Rol && User.IsInRole(tarea.AsignadoA)))
                        select new ListadoInicioContainer { Tarea = tarea, CotizacionExterno = cotiza, Mandante = usr };    
            

            if (!string.IsNullOrEmpty(search))
            {
                bandeja = bandeja.Where(x => x.Tarea.Solicitud.NumeroTicket.Contains(search) || x.Cotizacion.Cliente.Rut.Contains(search) || x.Cotizacion.Cliente.Nombres.Contains(search));
            }

            BootstrapTableResult<ListadoInicioContainer> salida = new BootstrapTableResult<ListadoInicioContainer>();
            salida.total = bandeja.Count();
            salida.rows = bandeja.Skip(offset).Take(limit).ToList();
            
            return Ok(salida);
        }
        
    }
}