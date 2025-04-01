using Microsoft.AspNetCore.SignalR;

public class OrderHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public async Task SendOrderCreated(int orderId)
    {
        Console.WriteLine($"Sending OrderCreated event for OrderId: {orderId}");
        await Clients.All.SendAsync("OrderCreated", orderId);
    }

    public async Task SendOrderUpdated(int orderId)
    {
        Console.WriteLine($"Sending OrderUpdated event for OrderId: {orderId}");
        await Clients.All.SendAsync("OrderUpdated", orderId);
    }

    public async Task SendOrderDeleted(int orderId)
    {
        Console.WriteLine($"Sending OrderDeleted event for OrderId: {orderId}");
        await Clients.All.SendAsync("OrderDeleted", orderId);
    }
}