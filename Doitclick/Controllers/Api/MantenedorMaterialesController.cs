using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Doitclick.Models.Application;
using Doitclick.Services.Workflow;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers.Api
{
    [Route("api/mantenedor/materiales")]
    [ApiController]
    public class MantenedorMaterialesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public MantenedorMaterialesController(ApplicationDbContext context )
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> GuardarIngresoMateriales([FromBody]  Doitclick.Models.Helper.FormularioMantenedorMateriales material)
        {
            //Generar modelo de cliente que en este caso es un paciente que viene a la oficina
            MaterialDisponible _material = new MaterialDisponible
            {
                Nombre = material.NombreMaterial,
                UnidadMedida = _context.TiposUnidadMedidas.Find(material.sslUnidadMedida),
                PrecioUnidad = material.PrecioMaterial,
                StockAlerta=material.StockMaterial,
                Codigo = material.Codigo,
                Marca = await _context.Marcas.FindAsync(material.Marca),
                Activa = true
            };
            
            _context.MaterialesDiponibles.Add(_material);
            var respuesta = await _context.SaveChangesAsync();

            return Ok("Datos guardados");

        }

        [HttpGet]
        public ActionResult<ICollection<MaterialDisponible>> ListarMateriales()
        {
            return _context.MaterialesDiponibles.Include(d => d.UnidadMedida).Include(m => m.Marca).ToList();
        }
        


    }
}