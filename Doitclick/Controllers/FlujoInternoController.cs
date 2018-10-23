using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Doitclick.Services.Workflow;

namespace Doitclick.Controllers
{
    [Authorize]
    public class FlujoInternoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkflowService _wfservice;
        public FlujoInternoController(ApplicationDbContext context, IWorkflowService wfservice)
        {
            _context = context;
            _wfservice = wfservice;
        }


        public IActionResult IngresoDatosPaciente(string ticket = "")
        {
            ViewBag.Servicios = _context.Servicios.ToList();
            ViewBag.PrevisionesSalud = _context.PrevisionesSalud.ToList();
            return View();
        }

        public IActionResult AsignarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();
            var laboratoristas = 
            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult EvaluarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();
            
            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult AsignarCotizacion(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult EvaluarCotizacion(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult InformeRechazo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            var esPredeterminado = _wfservice.ObtenerVariable("ES_TRABAJO_PREDETERMINADO", ticket);
            ViewBag.motivoRechazo = esPredeterminado == "1" ? _wfservice.ObtenerVariable("MOTIVO_REPARO_TRABAJO", ticket) : _wfservice.ObtenerVariable("MOTIVO_REPARO_COTZACION", ticket);

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult EjecutarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.ExisteReparo = _wfservice.ObtenerVariable("TRABAJO_CON_REPAROS_CC",ticket);
            ViewBag.MotivoReparo = _wfservice.ObtenerVariable("MOTIVO_REPARO_CC",ticket);

            return View();
        }

        public IActionResult CobroServicio(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            return View();
        }

        public IActionResult ControlCalidad(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.ExisteReparo = _wfservice.ObtenerVariable("TRABAJO_CON_REPAROS_CC",ticket);
            ViewBag.MotivoReparo = _wfservice.ObtenerVariable("MOTIVO_REPARO_CC",ticket);

            return View();
        }

        public IActionResult EntregaServicio(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            
            return View();
        }

        public IActionResult Cotizaciones()
        {
            return View();
        }

        
    }
}