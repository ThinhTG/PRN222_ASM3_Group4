using BusinessObject.Models;

namespace Services.InterfaceService;

public interface IOrderService
{
    Task<Order> GetOrderById(int orderId);
    Task<IEnumerable<Order>> GetAllOrders();
    Task<IEnumerable<Order>> GetAllOrdersByMemberId(int memberId);
    Task CreateOrder(Order order);
    Task<int> CreateOrderReturn(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
    Task<IEnumerable<Order>> GetSalesReport(DateTime startDate, DateTime endDate);

    Task<decimal> GetTotalPriceByOrderId(int orderId);
}



