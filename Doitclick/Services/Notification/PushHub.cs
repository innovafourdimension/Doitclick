using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Doitclick.Models.Security;
using System.Security.Claims;
using Doitclick.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Doitclick.Services.Notification
{
    [Authorize]
    public class PushHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        public PushHub(ApplicationDbContext context, UserManager<Usuario> userManager):base()
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        { 
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }


        public override Task OnConnectedAsync()
        {
            var rut = Context.User.Identity.Name;
            string connectionId = Context.ConnectionId;
            var usuarioLogeado = _context.Users.FirstOrDefault(u => u.Identificador == rut);
            _userManager.AddClaimAsync(usuarioLogeado, new Claim(JwtRegisteredClaimNames.Sid, connectionId));

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        

        /*public async Task NotificarCotizacionEntrante(Usuario usuario, string numeroTicket)
        {
            await Clients.User(usuario.Identificador).SendAsync("NotificarCotizacionEntrante", numeroTicket);
        }

        public async Task NotificarCotizacionSaliente(Usuario usuario, string numeroTicket)
        {
            await Clients.User(usuario.Identificador).SendAsync("NotificarCotizacionSaliente", numeroTicket);
        }*/

    }
}
