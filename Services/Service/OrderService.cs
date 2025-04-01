using AutoMapper;
using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Services.InterfaceService;

namespace Services.Service;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Order> GetOrderById(int orderId)
    {
        return await _orderRepository.GetOrderById(orderId);
    }

    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        return await _orderRepository.GetAllOrders();
    }

    public async Task CreateOrder(Order order)
    {
        await _orderRepository.AddOrder(order);
    }

    public async Task<int> CreateOrderReturn(Order order)
    {
        return await _orderRepository.AddOrderReturn(order);
    }

    public async Task UpdateOrder(Order order)
    {
        var existingOrder = await _orderRepository.GetOrderById(order.OrderId);
        if (existingOrder != null)
        {
            _mapper.Map(order, existingOrder);
            await _orderRepository.UpdateOrder(existingOrder);
        }
    }

    public async Task DeleteOrder(int orderId)
    {
        await _orderRepository.DeleteOrder(orderId);
    }

    public async Task<IEnumerable<Order>> GetSalesReport(DateTime startDate, DateTime endDate)
    {
        return await _orderRepository.GetOrdersByDateRange(startDate, endDate);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersByMemberId(int memberId)
    {
        return await _orderRepository.GetAllOrdersByMemberId(memberId);
    }
}
