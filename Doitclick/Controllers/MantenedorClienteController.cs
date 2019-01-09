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
    [Authorize]
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
        public IActionResult Formulario(int id=0)
        {
            var editando = id > 0;
            ViewBag.editando = editando;
            ViewBag.tiposList = (TipoCliente[])Enum.GetValues(typeof(TipoCliente));
            ViewBag.previsionesSalud = _context.PrevisionesSalud.Where(x => x.Activa == true).ToList();
            ViewBag.id = id;

            if(editando)
            {
                var cliente = _context.Clientes.Include(x => x.EntidadFacturacion).FirstOrDefault(cli => cli.Id == id);
                var telefono = _context.Contactos.Include(con => con.Cliente).FirstOrDefault(d => d.Cliente.Rut == cliente.Rut && d.TipoContacto == TipoContacto.TelefonoMovil); 
                var correo = _context.Contactos.Include(con => con.Cliente).FirstOrDefault(d => d.Cliente.Rut == cliente.Rut && d.TipoContacto == TipoContacto.CorreoElectronico); 
                ViewBag.telefono = telefono != null ? telefono.Resumen : "";
                ViewBag.correo = correo != null ? correo.Resumen : "";
                ViewBag.Cli = cliente;
                ViewBag.cliente = cliente;
            }
            
            
            return View();
        }
    }
}