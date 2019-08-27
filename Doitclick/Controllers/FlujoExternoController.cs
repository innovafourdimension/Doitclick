using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Doitclick.Services.Workflow;
using Rotativa.AspNetCore;
using Doitclick.Models.Helper;
using Microsoft.AspNetCore.Identity;
using Doitclick.Models.Security;
using Doitclick.Models.Application;

namespace Doitclick.Controllers
{
    [Authorize]
    [Route("procesos/ext")]
    public class FlujoExternoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkflowService _wfservice;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly string _nombreProceso = "FlujoExternos";
        public FlujoExternoController(UserManager<Usuario> userManager, RoleManager<Rol> roleManager, ApplicationDbContext context, IWorkflowService wfservice)
        {
            _context = context;
            _wfservice = wfservice;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Ingreso de Solicitud
        [HttpGet("ingreso-solicitud")]
        public IActionResult IngresoSolicitud()
        {
            ViewBag.Servicios = _context.Servicios.ToList();
            return View();
        }

        [HttpPost("ingreso-solicitud")]
        public async Task<IActionResult> IngresoSolicitud([FromBody] dynamic postData)
        {
            _context.Database.BeginTransaction();
            try{
                string cotizacionesPredeterminada = "1";
                string resumen = "Trabajo Externo: ";
                float _total = 0;
                var solicitudGen = _wfservice.Instanciar(_nombreProceso, User.Identity.Name, resumen);
                var _cliente = _context.Clientes.Include(cl => cl.EntidadFacturacion).FirstOrDefault(cli => cli.Rut == User.Identity.Name);
                CotizacionExterno _cotizaExterno = new CotizacionExterno
                {
                    Id = Guid.NewGuid().ToString(),
                    EntidadSolicitante = _cliente.EntidadFacturacion.Rut,
                    FechaCreacion = DateTime.Now,
                    RutPaciente = postData.RutPaciente,
                    NombrePaciente = postData.NombrePaciente,
                    OrdenFolio = postData.NroOrden,
                    OrdenImagen = postData.SrcImagen,
                    RequiereRetiro = postData.RequiereDelivery,
                    DireccionRetiro = postData.DireccionRetiro,
                    Resumen = postData.BreveDescripcion,
                    NumeroTicket = solicitudGen.NumeroTicket
                };

                foreach(var x in postData.Servicios)
                {
                    ItemCotizar _itm;
                    if (x.id.Equals("Otro"))
                    {
                        _itm = new ItemCotizar
                        {
                            CotizacionExterno = _cotizaExterno,
                            Descripcion = x.descripcion
                        };
                        cotizacionesPredeterminada = ResultTypes.Negative;
                    }
                    else
                    {
                        int _id = x.id;
                        _itm = new ItemCotizar
                        {
                            CotizacionExterno = _cotizaExterno,
                            Servicio = _context.Servicios.FirstOrDefault(s => s.Id == _id),
                            Cantidad = Convert.ToInt32(x.cantidad)
                        };
                        _total += _itm.Cantidad * _itm.Servicio.ValorCosto;
                    }
                    _context.ItemsCorizar.Add(_itm);
                }
                _cotizaExterno.PrecioTotal = _total;
                string delreq = _cotizaExterno.RequiereRetiro ? ResultTypes.Positive : ResultTypes.Negative;
                _wfservice.AsignarVariable("ES_TRABAJO_PREDETERMINADO", cotizacionesPredeterminada, solicitudGen.NumeroTicket);
                _wfservice.AsignarVariable("REQUERE_DELIVERY", delreq, solicitudGen.NumeroTicket);
                _wfservice.Avanzar(_nombreProceso, "INGRESO_SOLICITUD", solicitudGen.NumeroTicket, User.Identity.Name);
                var respuesta = await _context.SaveChangesAsync();
                _context.Database.CommitTransaction();
                return Ok(solicitudGen);
            }catch(Exception ex){
                _context.Database.RollbackTransaction();
                return BadRequest(ex.Message);
            }
            
        }
        
        #endregion


        [HttpGet("retiro-y-entrega/{numeroTicket?}")]
        public IActionResult RetiroYEntrega(string numeroTicket = ""){
            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);
            
            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            

            return View();
        }

        [HttpPost("retiro-y-entrega")]
        public IActionResult RetiroYEntrega([FromBody] dynamic postData)
        {
            string stepName = "RETIRO_Y_ENTREGA_INGRESADO";
            string numeroTicket = postData.numeroTicket;
            try
            {   
                _wfservice.Avanzar("FlujoExternos", stepName, numeroTicket, User.Identity.Name);
                return Ok(postData);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("recepcion/{numeroTicket?}")]
        public IActionResult Recepcion(string numeroTicket = ""){
            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            return View();
        }

        [HttpPost("recepcion")]
        public IActionResult Recepcion([FromBody] dynamic postData){

            string stepName = "RECEPCION_INGRESADO";
            string numeroTicket = postData.numeroTicket;
            try
            {
                _wfservice.Avanzar("FlujoExternos", stepName, numeroTicket, User.Identity.Name);
                return Ok(postData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("analisis-y-asignacion/{numeroTicket?}")]
        public async Task<IActionResult> AnalisisYAsignacion(string numeroTicket = ""){
            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);
            var laboratoristas = await _userManager.GetUsersInRoleAsync("Laboratorista");

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;
            ViewBag.laboratoristas = laboratoristas;

            return View();
        }

        [HttpPost("analisis-y-asignacion")]
        public IActionResult AnalisisYAsignacion([FromBody] dynamic postData){
            
            string stepName = "ANALISIS_Y_ASIGNACION_DE_SOLICITUD";
            try
            {
                _wfservice.AsignarVariable("TIPO_TRABAJO", (string)postData.tipoTrabajo, (string)postData.numeroTicket);
                _wfservice.AsignarVariable("LAB_ASIGNADO", (string)postData.laboratoristaAsignado, (string)postData.numeroTicket);
                _wfservice.AsignarVariable("COMENTARIOS_ANALISIS_ASIGNACION", (string)postData.comentarios, (string)postData.numeroTicket);
                _wfservice.Avanzar("FlujoExternos", stepName, (string)postData.numeroTicket, User.Identity.Name);

                return Ok(postData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Evaluacion de trabajo x parte del Laboratorista

        [HttpGet("evaluacion-trabajo/{numeroTicket?}")]
        public IActionResult Evaluacion(string numeroTicket = ""){

            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            return View();
        }

        [HttpPost("evaluacion-trabajo")]
        public IActionResult Evaluacion([FromBody] dynamic postData){
            string stepName = "ANALIZAR_Y_EVALUAR_TRABAJO";
            try
            {
                var sepuede = (string)postData.resultadoAnalisis == "S1" ? $"1" : $"0";
                _wfservice.AsignarVariable("RESULTADO_EVALUACION", (string)postData.resultadoAnalisis, (string)postData.numeroTicket);
                _wfservice.AsignarVariable("COMENTARIOS_EVALUACION", (string)postData.descipcionAnalisis, (string)postData.numeroTicket);
                _wfservice.AsignarVariable("SE_PUEDE_EJECUTAR", sepuede, (string)postData.numeroTicket);
                _wfservice.Avanzar("FlujoExternos", stepName, (string)postData.numeroTicket, User.Identity.Name);
                return Ok(postData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Ejecucion del trabajo
        [HttpGet("ejecucion-trabajo/{numeroTicket?}")]
        public IActionResult Ejecucion(string numeroTicket = ""){

            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);
            var trabajoIniciado = _wfservice.ObtenerVariable("MOMENTO_INICIO_TRABAJO", numeroTicket);

            

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;
            ViewBag.trabajoIniciado = trabajoIniciado;

            return View();
        }

        [HttpPost("ejecucion-trabajo/inicio")]
        public IActionResult EjecucionIniciar([FromBody] dynamic postData)
        {
            //#EJECUTAR_TRABAJO
            try
            {
                _wfservice.AsignarVariable("MOMENTO_INICIO_TRABAJO", DateTime.Now.ToString(), (string)postData.numeroTicket);
                
                return Ok(postData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpPost("ejecucion-trabajo/fin")]
        public IActionResult Ejecucion([FromBody] dynamic postData){
            return Ok(postData);
        }

        #endregion

        [HttpGet("vb-mandante/{numeroTicket?}")]
        public IActionResult VbMandante(string numeroTicket = ""){

            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            return View();
        }

        [HttpPost("vb-mandante")]
        public IActionResult VbMandante([FromBody] dynamic postData){
            return Ok(postData);
        }

        [HttpGet("vb-control/{numeroTicket?}")]
        public IActionResult VbControl(string numeroTicket = ""){

            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            return View();
        }

        [HttpPost("vb-control")]
        public IActionResult VbControl([FromBody] dynamic postData){
            return Ok(postData);
        }

        [HttpGet("preparar-despacho/{numeroTicket?}")]
        public IActionResult PrepararDespacho(string numeroTicket = ""){

            var cotizacion = _context.CotizacionesExternos.FirstOrDefault(x => x.NumeroTicket == numeroTicket);
            var entidadSolicitante = _context.EntidadesFacturacion.FirstOrDefault(d => d.Rut == cotizacion.EntidadSolicitante);
            var solicitud = _context.Solicitudes.FirstOrDefault(f => f.NumeroTicket == numeroTicket);
            var solicitante = _context.Users.FirstOrDefault(u => u.Identificador == solicitud.InstanciadoPor);

            ViewBag.cotizacion = cotizacion;
            ViewBag.entidadSolicitante = entidadSolicitante;
            ViewBag.solicitud = solicitud;
            ViewBag.solicitante = solicitante;

            return View();
        }

        [HttpPost("preparar-despacho")]
        public IActionResult PrepararDespacho([FromBody] dynamic postData){
            return Ok(postData);
        }
    }
}