using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    [Authorize]
    public class MantenedorRolController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorRolController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Listado()
        {
            var rols = _context.Roles.Include(s => s.Orzanizacion).ToList();
            ViewBag.rolesList = rols;
            return View();
        }

        public IActionResult Formulario(string id = "")
        {
            var rol = _context.Roles.Find(id);
            ViewBag.rol = rol;
            ViewBag.editando = !String.IsNullOrEmpty(id);
            ViewBag.OrganicacionesList = _context.Organizaciones.ToList();
            return View();
        }
    }
}