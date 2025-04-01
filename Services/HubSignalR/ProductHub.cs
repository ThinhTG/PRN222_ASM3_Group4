using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.HubSignalR
{
    public class ProductHub : Hub
    {
        public async Task ProductCreated()
        {
            await Clients.All.SendAsync("ProductCreated");
        }

        public async Task ProductUpdated()
        {
            await Clients.All.SendAsync("ProductUpdated");
        }

        public async Task ProductDeleted()
        {
            await Clients.All.SendAsync("ProductDeleted");
        }
    }
}
