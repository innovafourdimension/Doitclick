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
using Doitclick.Models.Helper;

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
        public async Task<IActionResult> GuardarIngresoMateriales([FromBody]  FormularioMantenedorMateriales material)
        {
            var mtrl = _context.MaterialesDiponibles.Find(material.Id);

            if(mtrl != null){
                mtrl.Marca = await _context.Marcas.FindAsync(material.Marca);
                mtrl.UnidadMedida = await _context.TiposUnidadMedidas.FindAsync(material.sslUnidadMedida);
                mtrl.Nombre = material.NombreMaterial;
                mtrl.PrecioUnidad = material.PrecioMaterial;
                mtrl.StockAlerta=material.StockMaterial;
                mtrl.Codigo = material.Codigo;
                 _context.MaterialesDiponibles.Update(mtrl);
            }else{
                mtrl = new MaterialDisponible
                {
                    Nombre = material.NombreMaterial,
                    UnidadMedida = _context.TiposUnidadMedidas.Find(material.sslUnidadMedida),
                    PrecioUnidad = material.PrecioMaterial,
                    StockAlerta=material.StockMaterial,
                    Codigo = material.Codigo,
                    Marca = await _context.Marcas.FindAsync(material.Marca),
                    Activa = true
                };
                _context.MaterialesDiponibles.Add(mtrl);
            }

            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpGet("[Action]/{Id}")]
        public async Task<IActionResult> EliminarMaterial([FromRoute]  string Id)
        {
            if(string.IsNullOrEmpty(Id))
            {
                return BadRequest("Parámetro Id Vacío");
            }

            var mtrl = _context.MaterialesDiponibles.Find(Convert.ToInt32(Id));
            if(mtrl != null){
                mtrl.Activa=false;
                await _context.SaveChangesAsync();
            }
            
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<MaterialDisponible>> ListarMateriales()
        {
            return _context.MaterialesDiponibles.Include(d => d.UnidadMedida).Include(m => m.Marca).ToList();
        }
        


    }
}