using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Security;
using Doitclick.Data;

namespace Doitclick.Controllers
{
    public class MantenedorTipoMedidaController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorTipoMedidaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            ViewBag.unidadlist = _context.TiposUnidadMedidas.Where(x => x.Activa == true).ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;

            var Unidad = _context.TiposUnidadMedidas.FirstOrDefault(x => x.Id == id);
            ViewBag.tipuni = Unidad;
            return View();
        }

        
        
    }
}