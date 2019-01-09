using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doitclick.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers
{
    public class MantenedorMaterialesController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorMaterialesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;
            ViewBag.editando = (id > 0);
            var material = _context.MaterialesDiponibles
                            .Include(d => d.UnidadMedida)
                            .Include(d => d.Marca)
                            .FirstOrDefault(m => m.Id == id);
            
            ViewBag.materialDisponible = material;

            if(id > 0){
                ViewBag.marcaListado = _context.Marcas.ToList();
                ViewBag.unimedListado =_context.TiposUnidadMedidas.ToList();
            }else{
                ViewBag.marcaListado = _context.Marcas.Where(x => x.Activa == true).ToList();
                ViewBag.unimedListado =_context.TiposUnidadMedidas.Where(x => x.Activa == true).ToList();
            }

            return View();  
        }

        public IActionResult Listado()
        {
            var listadoMateriales = _context.MaterialesDiponibles.Include(d => d.UnidadMedida).Include(m => m.Marca).Where(matd => matd.Activa == true).ToList();
            ViewBag.materialesDisponibles = listadoMateriales;
            return View();
        }
    }
}