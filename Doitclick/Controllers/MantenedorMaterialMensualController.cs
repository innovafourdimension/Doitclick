using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Doitclick.Models.Security;
using Doitclick.Data;
using Microsoft.EntityFrameworkCore;
using Doitclick.Models.Helper.Dto;
using Doitclick.Models.Application;

namespace Doitclick.Controllers
{
    public class MantenedorMaterialMensualController : Controller
    {

        private readonly ApplicationDbContext _context;
        public MantenedorMaterialMensualController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Listado()
        {
           ViewBag.matmens = _context.MaterialesMensuales.Include(x=>x.UnidadMedida).Where(x => x.Activa == true).ToList();
            return View();
        }

        public IActionResult Formulario(int id = 0)
        {
            ViewBag.Id = id;
            
            
            var materialmensual = _context.MaterialesMensuales.Include(x=>x.UnidadMedida).Include(x => x.Marca).FirstOrDefault(x => x.Id == id);
            ViewBag.mensual = materialmensual;
            
            ViewBag.editando = (id > 0);
            if(id > 0){
                ViewBag.marcaListado = _context.Marcas.ToList();
                ViewBag.unidadmed =_context.TiposUnidadMedidas.ToList();
            }else{
                ViewBag.marcaListado = _context.Marcas.Where(x => x.Activa == true).ToList();
                ViewBag.unidadmed =_context.TiposUnidadMedidas.Where(x => x.Activa == true).ToList();
            }
            
            return View();
        }


        #region Solicitudes
        public IActionResult GeneraSolicitud() 
        {
            ViewBag.materialesMensuales = _context.MaterialesMensuales.Include(x => x.UnidadMedida).Where(x => x.Activa == true).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult GuardarGeneraSolicitud([FromBody] GeneraSolicitudContainerDto entrada)
        {

            var cantidadSolicitudesNoFinalizadas = _context.SolicitudesMaterialesMensuales.Where(sol => sol.RutSolicitante == User.Identity.Name && (sol.Estado == "Pendiente" || sol.Estado == "Aprobado")).Count();

            if(cantidadSolicitudesNoFinalizadas == 3){
                return BadRequest("No puedes tener mas de 3 solicitudes sin confirmar");
            }

            SolicitudMaterialMensual nueva_solicitud = new SolicitudMaterialMensual();
            nueva_solicitud.Comentarios = entrada.comentarios;
            nueva_solicitud.Estado = "Pendiente";
            nueva_solicitud.FechaSolicitud = DateTime.Now;
            nueva_solicitud.RutSolicitante = User.Identity.Name;
            
            _context.SolicitudesMaterialesMensuales.Add(nueva_solicitud);

            foreach(var material in entrada.materiales){
                MaterialMensualSolicitado material_solicitado = new MaterialMensualSolicitado();
                material_solicitado.MaterialMensual = _context.MaterialesMensuales.Find(material.id);
                material_solicitado.CantidadMateriales = material.cantidad;
                material_solicitado.SolicitudMaterialMensual = nueva_solicitud;
                _context.MaterialesMensualesSolicitados.Add(material_solicitado);
            }

            _context.SaveChanges();

            return Ok(entrada);
        }

        public IActionResult ObtenerSolicitud(string id)
        {
            var solicitud = _context.SolicitudesMaterialesMensuales.Find(id);
            var materiales = _context.MaterialesMensualesSolicitados.Include(d => d.MaterialMensual).Where(m => m.SolicitudMaterialMensual == solicitud);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.RutSolicitante);
            var resolutor = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.RutResolutor);
            var contenedor = new {
                solicitud,
                materiales,
                solicitante,
                resolutor
            };
            return Ok(contenedor);
        }

        [HttpPost]
        public IActionResult ProcesarSolicitud([FromBody] ProcesaSolicitudDto entrada)
        {
            var solicitud = _context.SolicitudesMaterialesMensuales.Find(entrada.idSolicitud);
            var materiales = _context.MaterialesMensualesSolicitados.Include(d => d.MaterialMensual).Where(m => m.SolicitudMaterialMensual == solicitud);
            solicitud.ComentariosFin = entrada.comentarios;
            solicitud.FechaFinalizacion = DateTime.Now;
            solicitud.Estado = entrada.tipoResultado;
            solicitud.RutResolutor = User.Identity.Name;
            _context.SolicitudesMaterialesMensuales.Update(solicitud);

            if(entrada.tipoResultado == "Aprobado"){
                foreach(var material in materiales)
                {
                    var matMens = material.MaterialMensual;
                    matMens.RestarStock(material.CantidadMateriales);
                    _context.MaterialesMensuales.Update(matMens);
                }
            }

            _context.SaveChanges();
            return Ok();
        }

        public IActionResult ConfirmarSolicitud(string id)
        {
            var solicitud = _context.SolicitudesMaterialesMensuales.Find(id);
            solicitud.Estado = "Entregado";
            _context.SolicitudesMaterialesMensuales.Update(solicitud);
            _context.SaveChanges();
            return Ok();
        }

        public IActionResult BandejaSolicitante()
        {
            ViewBag.SolicitudesPendientes = _context.SolicitudesMaterialesMensuales.Where(x => (x.Estado == "Pendiente" || x.Estado == "Aprobado") && x.RutSolicitante == User.Identity.Name);
            ViewBag.SolicitudesHistoricas = _context.SolicitudesMaterialesMensuales.Where(x => (x.Estado == "Rechazado" || x.Estado == "Entregado") && x.RutSolicitante == User.Identity.Name);
            return View();
        }

        public IActionResult BandejaResolutor()
        {
            ViewBag.SolicitudesPendientes = _context.SolicitudesMaterialesMensuales.Where(x => x.Estado == "Pendiente");
            return View();
        }

        
        #endregion

        public IActionResult AgregarStock(int id = 0)
        {
            var matMensual = _context.MaterialesMensuales.Find(id);
            

            ViewBag.MaterialMensual = matMensual;
            return View();
        }

        [HttpPost]
        public IActionResult GuardarAgregarStock([FromBody] AgregarStockMaterialMensualDto entrada)
        {
            var matMens = _context.MaterialesMensuales.Find(entrada.id);
            matMens.AgregarStock(entrada.cantidad);
            _context.MaterialesMensuales.Update(matMens);
            _context.SaveChanges();
            return Ok();
        }
        
    }
}