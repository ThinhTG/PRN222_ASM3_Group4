using AutoMapper;
using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Microsoft.AspNetCore.SignalR;
using Services.InterfaceService;
using Services.HubSignalR;

namespace Services.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, IHubContext<OrderHub> hubContext)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _hubContext = hubContext;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await _orderRepository.GetOrderById(orderId);
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        var result = await _orderRepository.GetAllOrders();
        Console.WriteLine($"GetAllOrders returned {result.Count()} orders");
        foreach (var order in result)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Freight: {order.Freight}, Details: {order.OrderDetails?.Count ?? 0}");
        }
        return result;
    }

    public async Task CreateOrder(Order order)
    {
        int orderId = await _orderRepository.AddOrderReturn(order);
        order.OrderId = orderId;
        Console.WriteLine($"Sending OrderCreated event for OrderId: {order.OrderId}");
        await _hubContext.Clients.All.SendAsync("OrderCreated", order.OrderId);
    }

    public async Task<int> CreateOrderReturn(Order order)
    {
        int orderId = await _orderRepository.AddOrderReturn(order);
        await _hubContext.Clients.All.SendAsync("OrderCreated", orderId);
        return orderId;
    }

    public async Task UpdateOrder(Order order)
    {
        var existingOrder = await _orderRepository.GetOrderById(order.OrderId);
        if (existingOrder != null)
        {
            _mapper.Map(order, existingOrder);
            await _orderRepository.UpdateOrder(existingOrder);
            Console.WriteLine($"Sending OrderUpdated event for OrderId: {order.OrderId}");
            await _hubContext.Clients.All.SendAsync("OrderUpdated", order.OrderId);
        }
    }

    public async Task DeleteOrder(int orderId)
    {
        await _orderRepository.DeleteOrder(orderId);
        await _hubContext.Clients.All.SendAsync("OrderDeleted", orderId);
    }

    public async Task<IEnumerable<Order>> GetSalesReport(DateTime startDate, DateTime endDate)
    {
        return await _orderRepository.GetOrdersByDateRange(startDate, endDate);
    }

    public async Task<decimal> GetTotalPriceByOrderId(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);
        if (order == null || order.OrderDetails == null)
        {
            return 0;
        }

        return order.OrderDetails.Sum(od => od.UnitPrice * od.Quantity * (1 - (decimal)od.Discount));
    }



    public async Task<IEnumerable<Order>> GetAllOrdersByMemberId(int memberId)
    {
        return await _orderRepository.GetAllOrdersByMemberId(memberId);
    }

}

