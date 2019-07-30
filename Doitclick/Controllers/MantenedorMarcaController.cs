using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;

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