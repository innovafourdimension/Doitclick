using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Data;
using Microsoft.AspNetCore.Identity;
using Doitclick.Models.Security;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    public class MantenedorUsuarioController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        public MantenedorUsuarioController( ApplicationDbContext context, 
                                            RoleManager<Rol> roleManager, 
                                            UserManager<Usuario> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listado()
        {
            ViewBag.usuariosList = _context.Users.ToList();
            return View();
        }

        public async Task<IActionResult> Formulario(string id)
        {
            var user = await _context.Users.FindAsync(id);
            ViewBag.editando = !String.IsNullOrEmpty(id);
            //ViewBag.rolUsuario = await _userManager.GetRolesAsync(user);
            ViewBag.usuario = user;
            ViewBag.rolesList = _context.Roles.Include(r => r.Orzanizacion).ToList();
            return View();
        }
    }
}