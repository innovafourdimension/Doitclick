using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Doitclick.Models.Application;
using Doitclick.Services.Workflow;
using Doitclick.Models.Helper.Mantenedores;

namespace Doitclick.Controllers.Api
{
    [Route("api/mantenedor/servicios")]
    [ApiController]
    public class MantenedorServicioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public MantenedorServicioController(ApplicationDbContext context )
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> GuardarIngresoServicio([FromBody] ServicioFormularioIngreso servicios)
        {

            //Generar modelo de cliente que en este caso es un paciente que viene a la oficina
           Servicio _servicio = new Servicio
           {
                Nombre=servicios.NombreServicio,
                Resumen=servicios.DescripcionServicio,
                Codigo=servicios.CodigoServicio,
                ValorManoObra= 0,
                PorcentajeComision=0,
                Activa = true
                
           };

            _servicio.MaterialesPresupuestados = new List<MaterialPresupuestado>();
            float valorMateriales = 0;
            foreach(var x in servicios.Materiales){
                MaterialPresupuestado mp = new MaterialPresupuestado{
                    CantidadMaterial = x.Cantidad,
                    MaterialDisponible = _context.MaterialesDiponibles.FirstOrDefault(z => z.Id == x.MaterialId)
                };
                _servicio.MaterialesPresupuestados.Add(mp);
                valorMateriales += x.Precio;   
            }
            _servicio.ValorMateriales = Convert.ToInt32(valorMateriales);

            _context.Servicios.Add(_servicio);
            var respuesta = await _context.SaveChangesAsync();
            return Ok();
        }

       [HttpGet]
       public IActionResult ObtenerServiciosAll()
       {
           return Ok(_context.Servicios.Where(x => x.Activa == true).ToList());
       }
      
       
        

    }
}