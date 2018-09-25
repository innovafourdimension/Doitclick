using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Security;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    public class MantenedorInstrumentoController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorInstrumentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            ViewBag.instList = _context.Instrumentos.Include(f => f.Marca).Where(x => x.Activa == true).ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;
            var Inst = _context.Instrumentos.Include(x => x.Marca).FirstOrDefault(x => x.Id == id);
            ViewBag.instru = Inst;
            ViewBag.marcaListado = _context.Marcas.Where(x => x.Activa == true).ToList();
            ViewBag.editando = (id > 0);
            return View();
        }

        
        
    }
}