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
using Doitclick.Models.Helper;
using Doitclick.Models.Application;

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
            ViewBag.ProcesoId = procesoId;
            ViewBag.etapa = _context.Etapas.Include(d => d.Proceso).Include(e => e.Destinos).FirstOrDefault(e => e.Id == etapaId && e.Proceso.Id == procesoId);
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

        [HttpPost]
        public async Task<IActionResult> GardarEtapa([FromBody] EtapaHelper etapa)
        {

            if(etapa.id > 0)
            {
                Etapa etp = _context.Etapas.Include(f => f.Destinos).FirstOrDefault(x => x.Id == etapa.id);
                etp.Nombre = etapa.nombre;
                etp.NombreInterno = etapa.nombre.Replace(' ', '_').ToUpper();
                etp.Proceso = _context.Procesos.Find(etapa.proceso);
                etp.Secuencia = 900;
                etp.TipoDuracion = (TipoDuracion)Enum.Parse(typeof(TipoDuracion), etapa.tipoDuracion);
                etp.ValorDuracion =  etapa.valorDuracion;
                etp.TipoEtapa = (TipoEtapa)Enum.Parse(typeof(TipoEtapa), etapa.tipoEtapa);
                etp.TipoDuracionRetardo = (TipoDuracion)Enum.Parse(typeof(TipoDuracion), etapa.tipoDuracionRetardo);
                etp.ValorDuracionRetardo =  etapa.valorDuracion;
                etp.TipoUsuarioAsignado = (TipoUsuarioAsignado)Enum.Parse(typeof(TipoUsuarioAsignado), etapa.tipoUsuarioAsignado);
                etp.ValorUsuarioAsignado = etapa.valorUsuarioAsignado;
                etp.Enlace = etapa.enlace;

                foreach(var destino in etapa.destinos)
                {
                    if(destino.id > 0)
                    {
                        var transito = _context.Transiciones.Find(destino.id);
                        transito.NamespaceValidacion = destino.namespaceValidacion;
                        transito.ClaseValidacion = destino.claseValidacion;
                        transito.MetodoValidacion = destino.metodoValidacion;
                        transito.EtapaDestino = _context.Etapas.Find(destino.etapaDestino);
                        transito.EtapaActaual = etp;
                    }
                    else
                    {
                        _context.Transiciones.Add(new Transito{
                            NamespaceValidacion = destino.namespaceValidacion,
                            ClaseValidacion = destino.claseValidacion,
                            MetodoValidacion = destino.metodoValidacion,
                            EtapaDestino = _context.Etapas.Find(destino.etapaDestino),
                            EtapaActaual = etp
                        });
                    }
                }
                _context.Etapas.Update(etp);
                
            }
            else 
            {
                Etapa etp = new Etapa();
                etp.Nombre = etapa.nombre;
                etp.NombreInterno = etapa.nombre.Replace(' ', '_').ToUpper();
                etp.Proceso = _context.Procesos.Find(etapa.proceso);
                etp.Secuencia = 900;
                etp.TipoDuracion = (TipoDuracion)Enum.Parse(typeof(TipoDuracion), etapa.tipoDuracion);
                etp.ValorDuracion =  etapa.valorDuracion;
                etp.TipoEtapa = (TipoEtapa)Enum.Parse(typeof(TipoEtapa), etapa.tipoEtapa);
                etp.TipoDuracionRetardo = (TipoDuracion)Enum.Parse(typeof(TipoDuracion), etapa.tipoDuracionRetardo);
                etp.ValorDuracionRetardo =  etapa.valorDuracion;
                etp.TipoUsuarioAsignado = (TipoUsuarioAsignado)Enum.Parse(typeof(TipoUsuarioAsignado), etapa.tipoUsuarioAsignado);
                etp.ValorUsuarioAsignado = etapa.valorUsuarioAsignado;
                etp.Enlace = etapa.enlace;

                foreach(var destino in etapa.destinos)
                {
                    etp.Destinos.Add(new Transito{
                        NamespaceValidacion = destino.namespaceValidacion,
                        ClaseValidacion = destino.claseValidacion,
                        MetodoValidacion = destino.metodoValidacion,
                        EtapaDestino = _context.Etapas.Find(destino.etapaDestino)
                    });
                }
                _context.Etapas.Add(etp);
            }

            await _context.SaveChangesAsync();

            return Ok(etapa);
        }
    }
}