using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Doitclick.Models.Workflow;

namespace Doitclick.Controllers
{
    public class WorkflowAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public WorkflowAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Procesos()
        {
            ViewBag.procesos = _context.Procesos.ToList();
            return View();
        }

        //[Route("[Action]/{procesoId}")]
        public IActionResult EtapasFlujo(int procesoId)
        {
            ViewBag.proceso = _context.Procesos.Find(procesoId); 
            ViewBag.proceso.Etapas = _context.Etapas
                                                .Include(e=>e.Proceso)
                                                .Include(e=>e.Actuales)
                                                .Include(x=>x.Destinos)
                                                .Where(e=>e.Proceso.Id == procesoId).ToList();
            return View();
        }

        public IActionResult Etapa(int procesoId, int etapaId = 0)
        {
            ViewBag.Id = etapaId;
            ViewBag.etapa = _context.Etapas.Include(d => d.Proceso).Include(e => e.Destinos).FirstOrDefault(e => e.Id == etapaId);
            ViewBag.editando = etapaId > 0;
            ViewBag.tiposEtapa = Enum.GetValues(typeof(TipoEtapa)).Cast<TipoEtapa>().ToList();
            ViewBag.tiposAsignado = Enum.GetValues(typeof(TipoUsuarioAsignado)).Cast<TipoUsuarioAsignado>().ToList();
            ViewBag.tiposDuracion = Enum.GetValues(typeof(TipoDuracion)).Cast<TipoDuracion>().ToList();
            ViewBag.listadoEtapas = _context.Etapas.Include(d => d.Proceso).Where(e => e.Proceso.Id == procesoId);
            
            return View();
        }

        [HttpGet]
        public IActionResult DestinosEtapa(int etapaId){
            return Ok(_context.Transiciones.Include(e => e.EtapaDestino).Where(t => t.EtapaActaual.Id == etapaId).ToList());
        }

        public IActionResult GardarEtapa(Etapa etapa)
        {
            return Redirect("/WorkflowAdmin/Etapa?id=" + etapa.Id);
        }
    }
}