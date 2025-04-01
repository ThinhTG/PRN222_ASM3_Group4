using Microsoft.AspNetCore.SignalR;

namespace Services.HubSignalR
{
    public class Hubs : Hub
    {
		public async Task Created()
		{
			await Clients.All.SendAsync("Created");
		}

        public async Task Updated()
        {
            await Clients.All.SendAsync("Updated");
        }

        public async Task Deleted()
		{
			await Clients.All.SendAsync("Deleted");
		}
	}
}
