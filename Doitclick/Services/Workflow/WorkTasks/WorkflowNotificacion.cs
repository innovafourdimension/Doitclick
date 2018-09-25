using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Doitclick.Data;


namespace Doitclick.Services.Workflow.WorkTasks
{
    public class WorkflowNotificacion : WorkflowAutoTask
    {
        public WorkflowNotificacion(ApplicationDbContext context, string numeroTicket) : base(context, numeroTicket)
        {
        }
        private HubConnection connection;

        public override async Task Ejecutar()
        {
            try
            {
                /*connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hubs/push")
                .Build();
                

                await connection.StartAsync();
                await connection.InvokeAsync("SendMessage", "Charly", "Trabajo Ejecutado " + NumeroTicket);
                await connection.StopAsync();*/

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}
