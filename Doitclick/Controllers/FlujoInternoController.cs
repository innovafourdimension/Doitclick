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
    public class FlujoInternoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWorkflowService _wfservice;
        private readonly UserManager<Usuario> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        public FlujoInternoController(UserManager<Usuario> userManager, RoleManager<Rol> roleManager, ApplicationDbContext context, IWorkflowService wfservice)
        {
            _context = context;
            _wfservice = wfservice;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> VerCotizacion(string ticket)
        {
            var cotizacion = await _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefaultAsync();
            var Model = new CotizacionViewModel {
                Servicios = await _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToListAsync(),
                Cotizacion = cotizacion,
                DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante)
            };
        
            return View(Model);
            /*return new ViewAsPdf(Model){
                PageSize = Rotativa.AspNetCore.Options.Size.Letter
            };*/
        }

        public async Task<IActionResult> VerComprobante(string ticket)
        {
            var cotizacion = await _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefaultAsync();
            var cuentaCliente = await _context.CuentasCorrientes.Include(cc => cc.Cliente).Include(cc => cc.Movimientos).FirstOrDefaultAsync(cc => cc.Cliente.Id == cotizacion.Cliente.Id);
            var Model = new CotizacionViewModel {
                Servicios = await _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToListAsync(),
                Cotizacion = cotizacion,
                DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante),
                Movimientos = cuentaCliente.Movimientos.Where(mv => mv.NumeroTicket == cotizacion.NumeroTicket),    
            };
            Model.CalculoBalance = new CalculoBalance{
                Cargos = Model.Movimientos.Where(mov => mov.TipoTransanccion == TipoTransaccionCuentaCorriente.CargoCobroServicio || mov.TipoTransanccion == TipoTransaccionCuentaCorriente.EgresoReembolsoEfectivo).Sum(mov => mov.MontoTransaccion),
                Pagos = Model.Movimientos.Where(mov => mov.TipoTransanccion != TipoTransaccionCuentaCorriente.CargoCobroServicio && mov.TipoTransanccion != TipoTransaccionCuentaCorriente.EgresoReembolsoEfectivo).Sum(mov => mov.MontoTransaccion)
            };

            return View(Model);
            /*return new ViewAsPdf(Model){
                PageSize = Rotativa.AspNetCore.Options.Size.Letter
            };*/
        }


        public async Task<IActionResult> IngresoDatosPaciente(string ticket = "")
        {
            ViewBag.Servicios = _context.Servicios.ToList();
            ViewBag.PrevisionesSalud = _context.PrevisionesSalud.ToList();
            var rolDoctores = await _roleManager.FindByNameAsync("Cliente Externo");
            
            ViewBag.Externos = _userManager.Users.Where(u => u.Id  == _context.UserRoles.FirstOrDefault(r => r.RoleId == rolDoctores.Id && u.Id == r.UserId).UserId);
            return View();
        }

        public async Task<IActionResult> AsignarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();
            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> EvaluarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();
            
            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> AsignarCotizacion(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> EvaluarCotizacion(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> InformeRechazo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            var esPredeterminado = _wfservice.ObtenerVariable("ES_TRABAJO_PREDETERMINADO", ticket);
            ViewBag.motivoRechazo = esPredeterminado == "1" ? _wfservice.ObtenerVariable("MOTIVO_REPARO_TRABAJO", ticket) : _wfservice.ObtenerVariable("MOTIVO_REPARO_COTZACION", ticket);

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> EjecutarTrabajo(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            ViewBag.ExisteReparo = _wfservice.ObtenerVariable("TRABAJO_CON_REPAROS_CC",ticket);
            ViewBag.MotivoReparo = _wfservice.ObtenerVariable("MOTIVO_REPARO_CC",ticket);
            ViewBag.MomentoInicio = _wfservice.ObtenerVariable("MOMENTO_INICIO_TRABAJO", ticket);

            return View();
        }

        public async Task<IActionResult> CobroServicio(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public async Task<IActionResult> ControlCalidad(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            ViewBag.ExisteReparo = _wfservice.ObtenerVariable("TRABAJO_CON_REPAROS_CC",ticket);
            ViewBag.MotivoReparo = _wfservice.ObtenerVariable("MOTIVO_REPARO_CC",ticket);

            return View();
        }

        public async Task<IActionResult> EntregaServicio(string ticket)
        {
            var cotizacion = await _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefaultAsync();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();
            var cuentaCliente = await _context.CuentasCorrientes.Include(cc => cc.Cliente).Include(cc => cc.Movimientos).FirstOrDefaultAsync(cc => cc.Cliente.Id == cotizacion.Cliente.Id);
            
            ViewBag.CalculoBalance = new CalculoBalance{
                Cargos = cuentaCliente.Movimientos.Where(mov => mov.TipoTransanccion == TipoTransaccionCuentaCorriente.CargoCobroServicio || mov.TipoTransanccion == TipoTransaccionCuentaCorriente.EgresoReembolsoEfectivo).Sum(mov => mov.MontoTransaccion),
                Pagos = cuentaCliente.Movimientos.Where(mov => mov.TipoTransanccion != TipoTransaccionCuentaCorriente.CargoCobroServicio && mov.TipoTransanccion != TipoTransaccionCuentaCorriente.EgresoReembolsoEfectivo).Sum(mov => mov.MontoTransaccion)
            };


            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            ViewBag.Movimientos = cuentaCliente.Movimientos.Where(mv => mv.NumeroTicket == cotizacion.NumeroTicket);
            return View();
        }

        public async Task<IActionResult> ValidacionMandante(string ticket)
        {
            var cotizacion = _context.Cotizaciones.Include(x => x.Cliente).Where(c => c.NumeroTicket == ticket).FirstOrDefault();
            var servicios = _context.ItemsCorizar.Include(x => x.Servicio).Include(s => s.Cotizacion).Where(d => d.Cotizacion.Id == cotizacion.Id).ToList();

            ViewBag.Cotizacion = cotizacion;
            ViewBag.Servicios = servicios;
            ViewBag.DrMandante = await _userManager.FindByNameAsync(cotizacion.DrSolicitante);
            return View();
        }

        public IActionResult Cotizaciones()
        {
            return View();
        }

        public IActionResult VistaPacientesMandante()
        {
            return View();
        }

        
    }
}