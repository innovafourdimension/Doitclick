using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Models.Helper.Mantenedores;
using Doitclick.Models.Security;
using Doitclick.Models.Application;
using Doitclick.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Doitclick.Controllers.Api
{
    [Route("api/mantenedores")]
    [ApiController]
    public class MantenedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<Rol> _roleManager;
        private readonly UserManager<Usuario> _userManager;
        public MantenedoresController(  ApplicationDbContext context, 
                                        RoleManager<Rol> roleManager,
                                        UserManager<Usuario> userManager
        )
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Route("organizacion/guardar")]
        public IActionResult GuardarOrganzacion([FromBody] FormOrganizacion entrada)
        {
            Organizacion laOrganizacion = null;
            if(entrada.Id > 0)
            {
                laOrganizacion = _context.Organizaciones.Find(entrada.Id);
                laOrganizacion.Nombre = entrada.Nombre;
                laOrganizacion.TipoOrganizacion = Enum.Parse<TipoOrganizacion>(entrada.TipoOrganizacion);
            }
            else
            {
                laOrganizacion = new Organizacion();
                laOrganizacion.Nombre = entrada.Nombre;
                laOrganizacion.TipoOrganizacion = Enum.Parse<TipoOrganizacion>(entrada.TipoOrganizacion);
                _context.Organizaciones.Add(laOrganizacion);
            }
            _context.SaveChanges();

            return Ok("Salimos cauros!! " + entrada.Nombre);
        }

        [Route("organizacion/eliminar/{id}")]
        public IActionResult EliminarOrganzacion([FromRoute] int id)
        {
            _context.Organizaciones.Remove(_context.Organizaciones.Find(id));
            _context.SaveChanges();
            return Ok("Eliminamos cauros: " + id);
        }

        [Route("rol/guardar")]
        public async Task<IActionResult> GuardarRol([FromBody] FormRol entrada)
        {
            var orga = _context.Organizaciones.Where(org => org.Id == entrada.Organizacion).FirstOrDefault();
            Rol elRol =  _context.Roles.Find(entrada.Id);
            if(elRol != null)
            {

                elRol.Name = entrada.Nombre;
                elRol.Orzanizacion = orga;
                elRol.Comisionista = entrada.Comisionista;
                var result = await _roleManager.UpdateAsync(elRol);

                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                elRol = new Rol();
                elRol.Name = entrada.Nombre;
                elRol.Orzanizacion = orga;
                elRol.Comisionista = entrada.Comisionista;
                elRol.Activo = true;   
                var result = await _roleManager.CreateAsync(elRol); 

                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }        
            }
        }

        [Route("rol/eliminar/{id}")]
        public async Task<IActionResult> EliminarRol([FromRoute] string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return Ok("Eliminamos cauros: " + id);
        }

        [Route("usuario/guardar")]
        public async Task<IActionResult> GuardarUsuario([FromBody] FormUsuario entrada)
        {
            Usuario elUsuario = await _userManager.FindByNameAsync(entrada.Identificacion);
            // String.IsNullOrEmpty(elUsuario.UserName)
            if(elUsuario == null)
            {
                float prcc = !String.IsNullOrEmpty(entrada.PorcentajeComision) ? Convert.ToSingle(entrada.PorcentajeComision) : 0f; 
                var user = new Usuario { 
                    UserName = entrada.Identificacion, 
                    Email = entrada.Correo, 
                    Identificador = entrada.Identificacion, 
                    Nombres = entrada.Nombres,
                    PorcentajeComision = prcc,
                    PhoneNumber = entrada.Telefono
                };
                var result = await _userManager.CreateAsync(user, "Doitclick.01");

                if (result.Succeeded)
                {
                    var rs = await _userManager.AddToRoleAsync(user, entrada.Rol);
                    
                    if(rs.Succeeded){
                        return Ok("Correcto!");
                    }else{
                        return BadRequest(rs.Errors);
                    }
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            else
            {
                elUsuario.UserName = entrada.Identificacion;
                elUsuario.Nombres = entrada.Nombres;
                elUsuario.Email = entrada.Correo;
                elUsuario.PorcentajeComision = Convert.ToSingle(entrada.PorcentajeComision);

                var result = await _userManager.UpdateAsync(elUsuario);
                if (result.Succeeded)
                {
                    return Ok("Correcto!");
                }
                else
                {
                    return BadRequest(result.Errors);
                }        
            }
        }

        [Route("usuario/eliminar/{id}")]
        public async Task<IActionResult> EliminarUsuario([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return Ok("Eliminamos cauros: " + id);
        }
        
        [Route("servicios/eliminar/{id}")]
        public IActionResult EliminarServicio([FromRoute] int id)
        {
           try{
                //_context.Servicios.Remove();
                var svc = _context.Servicios.Find(id);
                svc.Activa = false;
                _context.SaveChanges();
                return Ok("Eliminamos cauros: " + id);
           }catch(Exception){
               return BadRequest("Error registo con dependencias");
           }
           
        }
        [Route("instrumento/guardar")]
        public IActionResult GuardarInstrumento([FromBody] FormInstrumento entrada)
        {
            Instrumento elInstrumento = null;
            if(entrada.Id > 0)
            {
                elInstrumento = _context.Instrumentos.Find(entrada.Id);
                elInstrumento.Codigo = entrada.CodigoInstrumento;
                elInstrumento.Nombre = entrada.NombreInstrumento;
                elInstrumento.Marca = _context.Marcas.Find(entrada.Marca);
                elInstrumento.Estado = entrada.Estado;
                elInstrumento.Descripcion = entrada.Descripcion;
                
            }
            else
            {
                elInstrumento = new Instrumento();
                elInstrumento.Nombre = entrada.NombreInstrumento;
                elInstrumento.Codigo = entrada.CodigoInstrumento;
                elInstrumento.Marca = _context.Marcas.Find(entrada.Marca);
                elInstrumento.Estado = entrada.Estado;
                elInstrumento.Descripcion = entrada.Descripcion;
                elInstrumento.Activa = true;

                _context.Instrumentos.Add(elInstrumento);
            }
            _context.SaveChanges();

            return Ok();
        }
        
        [Route("instrumento/eliminar/{id}")]
        public IActionResult Eliminarinstrumento([FromRoute] int id)
        {
            //_context.Instrumentos.Remove(_context.Instrumentos.Find(id));
            var instrumento = _context.Instrumentos.Find(id);
            instrumento.Activa = false;
            _context.SaveChanges();
            return Ok("Instrumento Eliminado");
        }
         [Route("tipounidadmedida/guardar")]
        public IActionResult GuardarTipoMedida([FromBody] FormTipoMedida entrada)
        {
            TipoUnidadMedida eltipounidadmedida = null;
            if(entrada.Id > 0)
            {
                eltipounidadmedida = _context.TiposUnidadMedidas.Find(entrada.Id);
                eltipounidadmedida.Nombre = entrada.NombreTipoMedida;
                
            }
            else
            {
                eltipounidadmedida = new TipoUnidadMedida();
                eltipounidadmedida.Nombre = entrada.NombreTipoMedida;
                eltipounidadmedida.Activa = true;
                _context.TiposUnidadMedidas.Add(eltipounidadmedida);
            }
            _context.SaveChanges();

            return Ok("Datos Guardados");
        }

        [Route("tipounidadmedida/eliminar/{id}")]
        public IActionResult EliminarTipoMedida([FromRoute] int id)
        {
            //_context.TiposUnidadMedidas.Remove(_context.TiposUnidadMedidas.Find(id));
            var unidad = _context.TiposUnidadMedidas.Find(id);
            unidad.Activa = false;
            _context.SaveChanges();
            return Ok("Unidad Medida Eliminado");
        }
        [Route("materialmensual/guardar")]
        public IActionResult GuardarMaterialMensual([FromBody] FormMaterialMensual entrada)
        {
            MaterialMensual elmaterialmensual = null;
            if(entrada.Id > 0)
            {
                elmaterialmensual= _context.MaterialesMensuales.Find(entrada.Id);
                elmaterialmensual.Nombre=entrada.Nombre;
                elmaterialmensual.Descripcion=entrada.Descripcion;
                elmaterialmensual.UnidadMedida=_context.TiposUnidadMedidas.Find(entrada.sslUnidadMedida);
                elmaterialmensual.Cantidad=entrada.Cantidad;
                elmaterialmensual.StockAlerta=entrada.Stock;
                elmaterialmensual.Codigo = entrada.Codigo;
                elmaterialmensual.Marca = _context.Marcas.Find(entrada.Marca);
                
            }
            else
            {
                elmaterialmensual = new MaterialMensual();
                elmaterialmensual.Nombre = entrada.Nombre;
                elmaterialmensual.Descripcion=entrada.Descripcion;
                elmaterialmensual.UnidadMedida=_context.TiposUnidadMedidas.Find(entrada.sslUnidadMedida);
                elmaterialmensual.Cantidad=entrada.Cantidad;
                elmaterialmensual.StockAlerta=entrada.Stock;
                elmaterialmensual.Codigo = entrada.Codigo;
                elmaterialmensual.Activa = true;
                elmaterialmensual.Marca = _context.Marcas.Find(entrada.Marca);

                _context.MaterialesMensuales.Add(elmaterialmensual);
            }
            _context.SaveChanges();

            return Ok("Datos Guardados");
        }
        [Route("materialmensual/eliminar/{id}")]
        public IActionResult EliminarMaterialMensual([FromRoute] int id)
        {
            //_context.MaterialesMensuales.Remove();
            var material = _context.MaterialesMensuales.Find(id);
            material.Activa = false;
            _context.SaveChanges();
            return Ok("Material Eliminado");
        }

        [Route("cliente/guardar")]
        public IActionResult GuardarCliente([FromBody] FormCliente entrada)
        {
            Cliente elcliente = null;
            if(entrada.Id > 0)
            {
                elcliente= _context.Clientes.Find(entrada.Id);
                elcliente.Rut=entrada.RutCliente;
                elcliente.Nombres=entrada.NombreCliente;
                elcliente.TipoCliente=Enum.Parse<TipoCliente>(entrada.tipocliente);
                elcliente.EsPersonalidadJuridica=entrada.optradio==1;
                elcliente.PrevisionSalud=_context.PrevisionesSalud.Find(entrada.prevision);
            }
            else
            {
                elcliente = new Cliente();
                elcliente.Rut=entrada.RutCliente;
                elcliente.Nombres=entrada.NombreCliente;
                elcliente.TipoCliente=Enum.Parse<TipoCliente>(entrada.tipocliente);
                elcliente.EsPersonalidadJuridica=(entrada.optradio==1);
                elcliente.PrevisionSalud=_context.PrevisionesSalud.Find(entrada.prevision);

                _context.Clientes.Add(elcliente);
            }
            _context.SaveChanges();

            return Ok("Datos Guardados");
        }
        [Route("cliente/eliminar/{id}")]
        public IActionResult EliminarCliente([FromRoute] int id)
        {
            _context.Clientes.Remove(_context.Clientes.Find(id));
            _context.SaveChanges();
            return Ok("Cliente Eliminado");
        }


        [Route("marcas/guardar")]
        public IActionResult GuardarMarca([FromBody] FormGenerico entrada)
        {
            Marca laMarca = null;
            if (entrada.Id > 0)
            {
                laMarca = _context.Marcas.Find(entrada.Id);
                laMarca.Nombre = entrada.Nombre;
                
            }
            else
            {
                laMarca = new Marca();
                laMarca.Nombre = entrada.Nombre;
                laMarca.Activa = true;
                _context.Marcas.Add(laMarca);
            }
            _context.SaveChanges();

            return Ok();
        }

        [Route("marcas/eliminar/{id}")]
        public IActionResult EliminarMarca([FromRoute] int id)
        {
            try{
                //_context.Marcas.Remove();
                var marca = _context.Marcas.Find(id);
                marca.Activa = false;
                _context.SaveChanges();
                return Ok("Marca Eliminada");
            }catch(DbUpdateException ex){
                return BadRequest(ex);
            }
        }
        
    }
}