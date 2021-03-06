﻿using System;
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
            laOrganizacion = new Organizacion();
            laOrganizacion.Nombre = entrada.Nombre;
            laOrganizacion.TipoOrganizacion = Enum.Parse<TipoOrganizacion>(entrada.TipoOrganizacion);
            _context.Organizaciones.Add(laOrganizacion);
            _context.SaveChanges();
            return Ok();
        }
        [Route("organizacion/editar")]
        public IActionResult EditarOrganzacion([FromBody] FormOrganizacion entrada)
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
            return BadRequest("Organizacion no editada");
            }
            _context.SaveChanges();
            return Ok();
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
                float prcc = !String.IsNullOrEmpty(entrada.PorcentajeComision) ? Convert.ToSingle(entrada.PorcentajeComision.Replace('.',',')) : 0f; 
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
                    
                    var rs = await _userManager.AddToRolesAsync(user, entrada.Rol);
                    
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
                elUsuario.PhoneNumber = entrada.Telefono;
                elUsuario.PorcentajeComision = !string.IsNullOrEmpty(entrada.PorcentajeComision) ? Convert.ToSingle(entrada.PorcentajeComision.Replace('.',',')) : 0f;
                var roledeslete = await _userManager.GetRolesAsync(elUsuario);
                if(roledeslete.Count > 0)
                {
                    var rs1 = await _userManager.RemoveFromRolesAsync(elUsuario, roledeslete);
                }
                var rs = await _userManager.AddToRolesAsync(elUsuario, entrada.Rol);

                var result = await _userManager.UpdateAsync(elUsuario);
                if (result.Succeeded)
                {
                    return Ok();
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
           
                elInstrumento = new Instrumento();
                elInstrumento.Nombre = entrada.NombreInstrumento;
                elInstrumento.Codigo = entrada.CodigoInstrumento;
                elInstrumento.Marca = _context.Marcas.Find(entrada.Marca);
                elInstrumento.Estado = entrada.Estado;
                elInstrumento.Descripcion = entrada.Descripcion;
                elInstrumento.Activa = true;

                _context.Instrumentos.Add(elInstrumento);
                _context.SaveChanges();

            return Ok();
        }
        //EDITAR.
        [Route("instrumento/editar")]
        public IActionResult EditarInstrumento([FromBody] FormInstrumento entrada)
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
               return BadRequest("Marca no editada");
            }
            _context.SaveChanges();
            return Ok();
        }
        //FIN EDITAR INSTRUMENTO

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
        public async Task<IActionResult> GuardarCliente([FromBody] FormCliente entrada)
        {
            Cliente elcliente = null;
            if(entrada.Id > 0)
            {
                elcliente = _context.Clientes.Find(entrada.Id);
                elcliente.Rut=entrada.rutCliente;
                elcliente.Nombres=entrada.nombreCliente;
                elcliente.TipoCliente=Enum.Parse<TipoCliente>(entrada.tipocliente);
                elcliente.EsPersonalidadJuridica=Convert.ToBoolean(entrada.esJuridico);
                elcliente.PrevisionSalud= elcliente.TipoCliente == TipoCliente.Paciente ? _context.PrevisionesSalud.Find(Convert.ToInt32(entrada.prevision)) : null;
                elcliente.EntidadFacturacion = _context.EntidadesFacturacion.FirstOrDefault(efa => efa.Rut == entrada.rutFacturacon);
                if(elcliente.EntidadFacturacion == null && elcliente.TipoCliente != TipoCliente.Paciente)
                {
                    elcliente.EntidadFacturacion = new EntidadFacturacion();
                    elcliente.EntidadFacturacion.Rut = entrada.rutFacturacon;
                    elcliente.EntidadFacturacion.RazonSocial  = entrada.razonSocialFacturacion;
                    elcliente.EntidadFacturacion.Direccion = entrada.direccionFacturacion;
                    elcliente.EntidadFacturacion.Giro = entrada.giroFacturacion;
                    elcliente.EntidadFacturacion.Telefono = entrada.telefonoFacturacion;
                }

                _context.Clientes.Update(elcliente);

            }
            else
            {
                elcliente = new Cliente();
                elcliente.Rut=entrada.rutCliente;
                elcliente.Nombres=entrada.nombreCliente;
                elcliente.TipoCliente=Enum.Parse<TipoCliente>(entrada.tipocliente);
                elcliente.EsPersonalidadJuridica=Convert.ToBoolean(entrada.esJuridico);
                elcliente.PrevisionSalud= elcliente.TipoCliente == TipoCliente.Paciente ? _context.PrevisionesSalud.Find(Convert.ToInt32(entrada.prevision)) : null;
                

                if(elcliente.TipoCliente != TipoCliente.Paciente){
                    elcliente.EntidadFacturacion = _context.EntidadesFacturacion.FirstOrDefault(efa => efa.Rut == entrada.rutFacturacon);
                    if(elcliente.EntidadFacturacion == null)
                    {
                        elcliente.EntidadFacturacion = new EntidadFacturacion();
                        elcliente.EntidadFacturacion.Rut = entrada.rutFacturacon;
                        elcliente.EntidadFacturacion.RazonSocial  = entrada.razonSocialFacturacion;
                        elcliente.EntidadFacturacion.Direccion = entrada.direccionFacturacion;
                        elcliente.EntidadFacturacion.Giro = entrada.giroFacturacion;
                        elcliente.EntidadFacturacion.Telefono = entrada.telefonoFacturacion;
                    }
                    Usuario elUsuario = await _userManager.FindByNameAsync(entrada.rutCliente);
                    if(elUsuario == null)
                    {
                        var user = new Usuario { 
                            UserName = entrada.rutCliente, 
                            Email = entrada.email, 
                            Identificador = entrada.rutCliente, 
                            Nombres = entrada.nombreCliente,
                            PhoneNumber = entrada.telefono
                        };
                        var result = await _userManager.CreateAsync(user, "Doitclick.01");

                        if (result.Succeeded)
                        {
                            
                            var rs = await _userManager.AddToRoleAsync(user, "Cliente Externo");
                        }
                        else
                        {
                            return BadRequest(result.Errors);
                        }
                    }
                }

                Contacto cntEmail = new Contacto{
                    Cliente = elcliente,
                    EsPrincipal = false,
                    TipoContacto = TipoContacto.CorreoElectronico,
                    Resumen = entrada.email
                };

                Contacto cntFono = new Contacto{
                    Cliente = elcliente,
                    EsPrincipal = false,
                    TipoContacto = TipoContacto.TelefonoMovil,
                    Resumen = entrada.telefono
                };
                
                _context.Contactos.Add(cntEmail);
                _context.Contactos.Add(cntFono);

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

                laMarca = new Marca();
                laMarca.Nombre = entrada.Nombre;
                laMarca.Activa = true;
                _context.Marcas.Add(laMarca);
           
            _context.SaveChanges();
            return Ok();
        }
         
        [Route("marcas/editar")]
        public IActionResult EditarMarca([FromBody] FormGenerico entrada)
        {
            Marca laMarca = null;
            if (entrada.Id > 0)
            {
                laMarca = _context.Marcas.Find(entrada.Id);
                laMarca.Nombre = entrada.Nombre;
                
            }
            else
            {
                return BadRequest("Marca no editada");
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