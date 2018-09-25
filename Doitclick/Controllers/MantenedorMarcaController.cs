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
    public class MantenedorMarcaController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorMarcaController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            ViewBag.marcasList = _context.Marcas.Where(x => x.Activa == true).ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;
            ViewBag.editando = (id > 0);
            ViewBag.marcaModel = _context.Marcas.Find(id);
            
            return View();
        }



    }
}