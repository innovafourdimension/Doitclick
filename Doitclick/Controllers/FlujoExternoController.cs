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
            }catch(Exception){
                _context.Database.RollbackTransaction();
                return BadRequest();
            }
            
        }
        
        [HttpGet("retiro-y-entrega")]
        public IActionResult RetiroYEntrega(){
            return View();
        }
    }
}