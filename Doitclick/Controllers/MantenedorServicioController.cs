using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    public class MantenedorServicioController : Controller
    {
        private readonly ApplicationDbContext _context;
      
        public MantenedorServicioController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Formulario(string id="")
        {
            ViewBag.MaterialesDisponibles = _context.MaterialesDiponibles.Where(x => x.Activa == true).Include(f => f.UnidadMedida).ToList();
            
            ViewBag.editando = !string.IsNullOrEmpty(id); 
            if(!string.IsNullOrEmpty(id))
            {
                var svc = _context.Servicios
                            .Include(sr => sr.MaterialesPresupuestados)
                            .ThenInclude(mp => mp.MaterialDisponible)
                            .ThenInclude(md => md.UnidadMedida)
                            .FirstOrDefault(sr => sr.Id == Convert.ToInt32(id));
                ViewBag.servicio = svc;
            }
            
            return View();
        }

        public IActionResult Listado()
        {
            ViewBag.servList = _context.Servicios.Where(x => x.Activa == true).ToList();//.Organizaciones.ToList();
            return View();
        }
    }
}