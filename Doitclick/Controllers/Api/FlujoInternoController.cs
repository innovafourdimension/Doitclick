using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Data;
using Doitclick.Models.Application;
using Doitclick.Services.Workflow;
using Doitclick.Models.Helper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Doitclick.Models.Workflow;

namespace Doitclick.Controllers.Api
{
    [Route("api/flujo-interno")]
    [ApiController]
    [Authorize]
    public class FlujoInternoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IWorkflowService _wfService;
        private readonly string _nombreProceso = "FlujoInterno";

        public FlujoInternoController(ApplicationDbContext context, IWorkflowService wfService)
        {
            _context = context;
            _wfService = wfService;
        }


        [Route("ingreso-datos-paciente")]
        [HttpPost]
        public async Task<IActionResult> GuardarIngresoDatosPaciente([FromBody]  FomularioIngresoDatosPaciente paciente)
        {
            int total = 0;
            string esPredeterminado = "1";
            //Genera instancia WF ya que aqui es donde empieza todo el proceso
            string resumen = "Paciente: " + paciente.RutPaciente + " - " + paciente.ApellidosPaciente + " " + paciente.NombrePaciente + ", Solicitda: " + paciente.DrSolicitante + ", Orden: " + paciente.NroOrden;
            var solicitudGen = _wfService.Instanciar(_nombreProceso, User.Identity.Name, resumen);
            #region Paciente
            //Generar modelo de cliente que en este caso es un paciente que viene a la oficina
            Cliente _paciente = new Cliente
            {
                Nombres = paciente.NombrePaciente + " " + paciente.ApellidosPaciente,
                Rut = paciente.RutPaciente,
                TipoCliente = TipoCliente.Paciente,
                EsPersonalidadJuridica = false,
                PrevisionSalud = _context.PrevisionesSalud.FirstOrDefault(p => p.Id == paciente.PrevisionSalud)
            };

            MetaDatosCliente _pacienteApellidos = new MetaDatosCliente
            {
                Cliente = _paciente,
                Clave = "Apellidos",
                Valor = paciente.ApellidosPaciente,
                Orden = 1
            };

            MetaDatosCliente _pacienteNombres = new MetaDatosCliente
            {
                Cliente = _paciente,
                Clave = "Nombres",
                Valor = paciente.NombrePaciente,
                Orden = 2
            };
            #endregion

            #region Telefono Movil
            Contacto _telMovil = new Contacto
            {
                Cliente = _paciente,
                EsPrincipal = true,
                TipoContacto=TipoContacto.TelefonoMovil,
                Resumen=paciente.Telefono,
                
            };

            #endregion

            #region Correo
            Contacto _correoPac = new Contacto
            {
                Cliente = _paciente,
                EsPrincipal = true,
                TipoContacto = TipoContacto.CorreoElectronico,
                Resumen = paciente.Correo,
            };

            #endregion
            


            Cotizacion _cotizza = new Cotizacion
            {
                Cliente = _paciente,
                DrSolicitante = paciente.DrSolicitante,
                EsOT = false,
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddDays(10),
                FolioSolicitante = paciente.NroOrden,
                Resumen = "Creado el " + DateTime.Now.ToString(),
                NumeroTicket = solicitudGen.NumeroTicket,
                ImagenOrdenSolicitante = paciente.SrcImagen,
                PrecioCotizacion = total
            };

            foreach(var x in paciente.Servicios)
            {
                ItemCotizar _itm;
                if (x.id.Equals("Otro"))
                {
                    _itm = new ItemCotizar
                    {
                        Cotizacion = _cotizza,
                        Descripcion = x.descripcion
                    };
                    esPredeterminado = "0";
                }
                else
                {
                    _itm = new ItemCotizar
                    {
                        Cotizacion = _cotizza,
                        Servicio = _context.Servicios.FirstOrDefault(s => s.Id == Convert.ToInt32(x.id)),
                        Cantidad = Convert.ToInt32(x.cantidad)
                    };
                    total += _itm.Cantidad * _itm.Servicio.ValorTotal;
                }
                _context.ItemsCorizar.Add(_itm);
            }
            _cotizza.PrecioCotizacion = total;
            //Guardar datos
            _context.Clientes.Add(_paciente);
            _context.MetadatosClientes.Add(_pacienteApellidos);
            _context.MetadatosClientes.Add(_pacienteNombres);
            _context.Contactos.Add(_telMovil);
            _context.Contactos.Add(_correoPac);
            _context.Cotizaciones.Add(_cotizza);
            var respuesta = await _context.SaveChangesAsync();

            _wfService.AsignarVariable("ES_TRABAJO_PREDETERMINADO", esPredeterminado, solicitudGen.NumeroTicket);
            _wfService.AsignarVariable("USUARIO_GENERA_OT", User.Identity.Name, solicitudGen.NumeroTicket);
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.IngresoDatosPaciente, solicitudGen.NumeroTicket, User.Identity.Name);
            return Ok(solicitudGen);

        }


        [Route("asignar-trabajo")]
        [HttpPost]
        public IActionResult GuardarAsignacionTrabajo([FromBody] ResultadoAsignacionDefault entrada)
        {
            _wfService.AsignarVariable("USUARIO_EVALUA_TRABAJO", entrada.UsuarioAsignado, entrada.NumeroTicket);
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.AsignarTrabajo, entrada.NumeroTicket, User.Identity.Name);
            
            return Ok();
        }


        [Route("asignar-cotizacion")]
        [HttpPost]
        public IActionResult GuardarAsignacionEvaluacion([FromBody] ResultadoAsignacionDefault entrada)
        {
            _wfService.AsignarVariable("USUARIO_EVALUA_COTIZACION", entrada.UsuarioAsignado, entrada.NumeroTicket);
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.AsignarCotizacion, entrada.NumeroTicket, User.Identity.Name);
            
            return Ok();
        }


        [Route("evaluar-trabajo")]
        [HttpPost]
        public IActionResult GuardarEvaluacionTrabajo([FromBody] ResultadoReparoDefault entrada)
        {
            _wfService.AsignarVariable("LABORATORISTA_ACEPTA_TRABAJO", entrada.Resultado.ToString().Equals("S") ? "1":"0", entrada.NumeroTicket);
            if(entrada.Resultado == "N")
            {
                _wfService.AsignarVariable("MOTIVO_REPARO_TRABAJO", entrada.MotivoReparo, entrada.NumeroTicket);
            }
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.EvaluarTrabajo, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }


        [Route("evaluar-cotizacion")]
        [HttpPost]
        public IActionResult GuardarEvaluacionCotizacion([FromBody] ResultadoReparoDefault entrada)
        {

            _wfService.AsignarVariable("LABORATORISTA_ACEPTA_COTIZACION", entrada.Resultado.Equals("S") ? "1":"0", entrada.NumeroTicket);
            if (entrada.Resultado == "N")
            {
                _wfService.AsignarVariable("MOTIVO_REPARO_COTZACION", entrada.MotivoReparo, entrada.NumeroTicket);
            }
            
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.EvaluarCotizacion, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }


        [Route("informe-rechazo")]
        [HttpPost]
        public IActionResult GuardarInformeRechazo([FromBody] ResultadoReparoDefault entrada)
        {
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.InformeRechazo, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }

        [Route("cobro-servicios")]
        [HttpPost]
        public IActionResult GuardarCobroServicios([FromBody] ResultadoCobroServicios entrada)
        {
            _wfService.AsignarVariable("CLIENTE_ACEPTA_PRESUPUESTO", entrada.AceptaPresupuesto.ToString(), entrada.NumeroTicket);
            if(entrada.AceptaPresupuesto > 0)
            {
                _wfService.AsignarVariable("MONTO_CANCELADO_INICIAL", entrada.ValorPagar.ToString(), entrada.NumeroTicket);
                _wfService.AsignarVariable("MONTO_TOTAL_SERVICIOS", entrada.ValorTotal.ToString(), entrada.NumeroTicket);

                var cotizacion = _context.Cotizaciones.Include(d => d.Cliente).FirstOrDefault(c => c.NumeroTicket == entrada.NumeroTicket);
                /* Existe cuenta corriente para el cliente */
                var cuentaCorriente = _context.CuentasCorrientes.FirstOrDefault(x => x.Cliente == cotizacion.Cliente);
                if(cuentaCorriente == null)
                {
                    cuentaCorriente = new CuentaCorriente{
                        Cliente = cotizacion.Cliente,
                        Numero = "9990" + cotizacion.Cliente.Rut,  
                    };
                    _context.CuentasCorrientes.Add(cuentaCorriente);
                }

                /*Genero cargo en la cuenta corriente */
                var movCobro = new MovimientoCuentaCorriente{
                    CuentaCorriente = cuentaCorriente,
                    FechaTransaccion = DateTime.Now,
                    MontoTransaccion  = entrada.ValorTotal,
                    NumeroTicket = entrada.NumeroTicket,
                    NumeroTransaccion = "C"+DateTime.Now.Ticks.ToString(),
                    Resumen = "Cargo de valor servicio de laboratorio prestado",
                    TipoTransanccion = TipoTransaccionCuentaCorriente.CargoCobroServicio   
                };   
                _context.MovimientosCuentasCorrientes.Add(movCobro);             

                /*Genero pago de cliente */
                var movPago = new MovimientoCuentaCorriente{
                    CuentaCorriente = cuentaCorriente,
                    FechaTransaccion = DateTime.Now,
                    MontoTransaccion  = entrada.ValorPagar,
                    NumeroTicket = entrada.NumeroTicket,
                    NumeroDocumento = entrada.NumeroDocumento.Length > 0 ? entrada.NumeroDocumento : null,
                    NumeroTransaccion = "A"+DateTime.Now.Ticks.ToString(),
                    Resumen = "Abono servicio de laboratorio prestado"
                }; 

                switch(entrada.FormaPago)
                {
                    case "EFC":
                        movPago.TipoTransanccion = TipoTransaccionCuentaCorriente.IngresoPagoEfectivo;
                        break;
                    case "TBK":
                        movPago.TipoTransanccion = TipoTransaccionCuentaCorriente.IngresoPagoTransbank;
                        break;
                    case "CHQ": 
                        movPago.TipoTransanccion = TipoTransaccionCuentaCorriente.IngresoPagoCheque;
                        break;
                }
                _context.MovimientosCuentasCorrientes.Add(movPago);

            }
            
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.CobroServicio, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }

        [Route("ejecutar-trabajo")]
        [HttpPost]
        public IActionResult GuardarEjecutarTrabajo([FromBody] ResultadoDefault entrada)
        {
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.EjecutarTrabajo, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }

        [Route("control-calidad")]
        [HttpPost]
        public IActionResult GuardarControlCalidad([FromBody] ResultadoReparoDefault entrada)
        {
            _wfService.AsignarVariable("TRABAJO_CON_REPAROS_CC", entrada.Resultado.Equals("S") ? "0":"1", entrada.NumeroTicket);
            if (entrada.Resultado == "N")
            {
                _wfService.AsignarVariable("MOTIVO_REPARO_CC", entrada.MotivoReparo, entrada.NumeroTicket);
            }
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.ControlCalidad, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }

        [Route("entrega-servicio")]
        [HttpPost]
        public IActionResult GuardarEntregaServicio([FromBody] ResultadoDefault entrada)
        {
            _wfService.Avanzar(_nombreProceso, EtapasFlujoInterno.EntregaServicio, entrada.NumeroTicket, User.Identity.Name);
            return Ok();
        }

        [Route("cotizaciones")]
        [HttpGet]
        public IActionResult ListarCotizaciones(int limit = 10, int offset = 0, string search = "")
        {
            var rut = User.Identity.Name;
            var bandeja = from solicitud in _context.Solicitudes
                          join cotiza in _context.Cotizaciones
                          .Include(x=>x.Cliente) on solicitud.NumeroTicket equals cotiza.NumeroTicket
                          where solicitud.Proceso.Id == 1 && solicitud.Estado == EstadoSolicitud.Finalizada
                          select new HistorialCerradasContainer{ Solicitud = solicitud, Cotizacion = cotiza };
            
            if (!string.IsNullOrEmpty(search))
            {
                bandeja = bandeja.Where(x => x.Solicitud.NumeroTicket.Contains(search) || x.Cotizacion.Cliente.Rut.Contains(search) || x.Cotizacion.Cliente.Nombres.Contains(search));
            }

            BootstrapTableResult<HistorialCerradasContainer> salida = new BootstrapTableResult<HistorialCerradasContainer>();
            salida.total = bandeja.Count();
            salida.rows = bandeja.Skip(offset).Take(limit).ToList();
            
            return Ok(salida);

        }


        [Route("detalle-cotizacion")]
        [HttpGet]
        public IActionResult DetalleCotizacion(string ticket)
        {
            var salida = _context.Tareas
                        .Include(tarea => tarea.Etapa)    
                        .Where(tarea => tarea.Solicitud.NumeroTicket == ticket).ToList();
            return Ok(salida);
        }



        [Route("listado-pacientes")]
        [HttpGet]
        public IActionResult ListarPacientes()
        {

            var clientes = _context.Clientes.ToList();

            
            return Ok(clientes);

        }

        [Route("bandeja-tareas")]
        [HttpGet]
        public IActionResult BandejaTareas(int limit = 10, int offset = 0, string search = "")
        {

            var rut = User.Identity.Name;
            var bandeja = from tarea in _context.Tareas
                          .Include(t => t.Solicitud).ThenInclude(s => s.Proceso)
                          .Include(t => t.Etapa)
                          join cotiza in _context.Cotizaciones
                          .Include(x=>x.Cliente) on tarea.Solicitud.NumeroTicket equals cotiza.NumeroTicket
                          where tarea.Solicitud.Proceso.Id == 1 && tarea.Estado == EstadoTarea.Activada && tarea.AsignadoA == rut
                          select new ListadoInicioContainer { Tarea = tarea, Cotizacion = cotiza };
                          

            
            if (!string.IsNullOrEmpty(search))
            {
                bandeja = bandeja.Where(x => x.Tarea.Solicitud.NumeroTicket.Contains(search) || x.Cotizacion.Cliente.Rut.Contains(search) || x.Cotizacion.Cliente.Nombres.Contains(search));
            }

            BootstrapTableResult<ListadoInicioContainer> salida = new BootstrapTableResult<ListadoInicioContainer>();
            salida.total = bandeja.Count();
            salida.rows = bandeja.Skip(offset).Take(limit).ToList();
            
            return Ok(salida);
        }
    }
}