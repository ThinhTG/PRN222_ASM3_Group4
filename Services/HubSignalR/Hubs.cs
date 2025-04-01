using Microsoft.AspNetCore.SignalR;

namespace Services.HubSignalR
{
    public class Hubs : Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("Update");
        }
    }
}
