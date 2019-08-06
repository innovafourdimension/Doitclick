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
   
    public class ListadoLaboratorioController : Controller
    {
         private readonly ApplicationDbContext _context;
        public ListadoLaboratorioController(ApplicationDbContext context)
        {
            _context = context;
        }
          public IActionResult Listado()
        {
            return View();
        }
       
    }
}