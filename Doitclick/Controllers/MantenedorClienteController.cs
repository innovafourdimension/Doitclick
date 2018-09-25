using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Application;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MantenedorClienteController : Controller
    {
         private readonly ApplicationDbContext _context;
        public MantenedorClienteController(ApplicationDbContext context)
        {
            _context = context;
        }
          public IActionResult Listado()
        {
            ViewBag.clienList = _context.Clientes.Include(x=>x.PrevisionSalud).ToList();
            return View();
        }
        public IActionResult Formulario(int id=0)//para listar los combos
        {
            ViewBag.Id = id;

            var Clien = _context.Clientes.FirstOrDefault(x => x.Id == id);
            ViewBag.Cli = Clien;


            ViewBag.tiposList = (TipoCliente[])Enum.GetValues(typeof(TipoCliente));
            ViewBag.PrevisionesSalud = _context.PrevisionesSalud.Where(x => x.Activa == true).ToList();
            return View();
        }
    }
}