using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    [Authorize]
    public class FlujoInternoController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FlujoInternoController(ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult EjecutarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
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
    }
}