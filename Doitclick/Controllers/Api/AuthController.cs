using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Doitclick.Models.Security;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Doitclick.Data;

namespace Doitclick.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<Rol> _roleManager;
        private readonly ApplicationDbContext _context;
        

        public AuthController(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            RoleManager<Rol> roleManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;
            
        }



        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] InfoUsuario infoUsuario)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(infoUsuario.Identificacion, infoUsuario.Llave, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return BuildToken(infoUsuario);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Datos de Acceso Invalidos.");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private IActionResult BuildToken(InfoUsuario infoUsuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, infoUsuario.Identificacion),
                new Claim("Algo", "caca"),
                //new Claim("Correo", infoUsuario.Correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Llave"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddHours(1);
            

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "yourdomain.com",
               audience: "yourdomain.com",
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration
            });

        }


        [Route("User/Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] InfoUsuario model)
        {

            if (ModelState.IsValid)
            {
                var user = new Usuario { UserName = model.Identificacion, Email = model.Correo, Identificador = model.Identificacion, Nombres = model.Nombres};
                var result = await _userManager.CreateAsync(user, model.Llave);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [Route("User/CreateNew")]
        [HttpPost]
        public async Task<IActionResult> CreateNewUser([FromBody] InfoUsuario model)
        {

            if (ModelState.IsValid)
            {
                var user = new Usuario { UserName = model.Identificacion, Email = model.Correo, Identificador = model.Identificacion, Nombres = model.Nombres };
                var result = await _userManager.CreateAsync(user, model.Llave);
                if (result.Succeeded)
                {
                    return BuildToken(model);
                }
                else
                {
                    return BadRequest("Username or password invalid");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


        [Route("Role/Create")]
        [HttpPost]
        public async Task<IActionResult> CreaRol([FromBody] InfoRol model)
        {
            if (ModelState.IsValid)
            {
                var orga = _context.Organizaciones.Where(org => org.Id == model.OrganizacionId).FirstOrDefault();

                //Padre = !string.IsNullOrEmpty(model.PadreId) ? _context.Roles.Where(role => role.Id == model.PadreId).FirstOrDefault() : null,
                var rl = new Rol
                {
                    Name = model.Nombre,
                    Orzanizacion = orga,
                    Activo = true
                };

                var result = await _roleManager.CreateAsync(rl);

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
                return BadRequest("Algo anda mal");
            }
        }
        

        [Route("poner-online")]
        [HttpGet]
        public async Task<IActionResult> PonerOnline(string id)
        {

            var rut = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.UniqueName).Value;
            var usuarioLogeado = _context.Users.FirstOrDefault(u => u.Identificador == rut);
            
            await _userManager.AddClaimAsync(usuarioLogeado, new Claim(JwtRegisteredClaimNames.Sid, id));
            return Ok("...Data Procesada");
        }
    }
}