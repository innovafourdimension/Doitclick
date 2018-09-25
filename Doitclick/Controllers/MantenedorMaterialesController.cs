using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Microsoft.AspNetCore.Mvc;

namespace Doitclick.Controllers
{
    public class MantenedorMaterialesController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorMaterialesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Formulario()
        {
            ViewBag.marcaListado = _context.Marcas.Where(x => x.Activa == true).ToList();
            ViewBag.unimedListado = _context.TiposUnidadMedidas.Where(x => x.Activa == true).ToList();
            return View();  
        }
    }
}