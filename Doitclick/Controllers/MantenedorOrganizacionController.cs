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
    public class MantenedorOrganizacionController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorOrganizacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
            ViewBag.orgaList = _context.Organizaciones.ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;

            var Orga = _context.Organizaciones.FirstOrDefault(x => x.Id == id);
            ViewBag.Org = Orga;
            
            ViewBag.tiposList = (TipoOrganizacion[])Enum.GetValues(typeof(TipoOrganizacion));
            return View();
        }

        
        
    }
}