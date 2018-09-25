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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using Doitclick.Models.Workflow;

namespace Doitclick.Controllers.Api
{
    [Route("api/flujo-interno")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FlujoInternoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IWorkflowService _wfService;

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
            var solicitudGen = _wfService.Instanciar("FlujoPruebas", "17042783-1", resumen);
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
            _wfService.Avanzar("FlujoPruebas", EtapasFlujoInterno.IngresoDatosPaciente, solicitudGen.NumeroTicket, "17042783-1");
            return Ok("Datos guardados");

        }



        [Route("asignar-trabajo")]
        [HttpPost]
        public IActionResult GuardarAsignacionTrabajo([FromBody] ResultadoAsignacionDefault entrada)
        {

            _wfService.AsignarVariable("RESPONSABLE_TRABAJO", entrada.UsuarioAsignado, entrada.NumeroTicket);
            
            _wfService.Avanzar("FlujoPruebas", EtapasFlujoInterno.AsignarTrabajo, entrada.NumeroTicket, User.Identity.Name);


            return Ok("Datos guardados");

        }


        [Route("evaluar-trabajo")]
        [HttpPost]
        public IActionResult GuardarEvaluacionTrabajo([FromBody] ResultadoReparoDefault entrada)
        {

            _wfService.AsignarVariable("TRABAJO_REALIZABLE", entrada.Resultado.ToString(), entrada.NumeroTicket);
            if(entrada.Resultado == "N")
            {
                _wfService.AsignarVariable("MOTIVO_REPARO_TRABAJO", entrada.MotivoReparo, entrada.NumeroTicket);
            }
            _wfService.Avanzar("FlujoPruebas", EtapasFlujoInterno.IngresoDatosPaciente, entrada.NumeroTicket, User.Identity.Name);


            return Ok("Datos guardados");

        }


        [Route("evaluar-cotizacion")]
        [HttpPost]
        public IActionResult GuardarEvaluacionCotizacion([FromBody] ResultadoReparoDefault entrada)
        {

            _wfService.AsignarVariable("COTIZACION_REALIZABLE", entrada.Resultado.ToString(), entrada.NumeroTicket);
            if (entrada.Resultado == "N")
            {
                _wfService.AsignarVariable("MOTIVO_REPARO_COTZACION", entrada.MotivoReparo, entrada.NumeroTicket);
            }
            _wfService.Avanzar("FlujoPruebas", EtapasFlujoInterno.AsignarCotizacion, entrada.NumeroTicket, User.Identity.Name);


            return Ok("Datos guardados");

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