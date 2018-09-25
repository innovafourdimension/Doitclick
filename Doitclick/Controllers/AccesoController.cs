using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Doitclick.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Denegado()
        {
            return Unauthorized();
        }


        public IActionResult Add()
        {
            return View();
        }
    }
}