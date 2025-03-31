using BusinessObject.Models;

namespace Services.InterfaceService;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrderById(int orderId);
    Task CreateOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
    Task<IEnumerable<Order>> GetSalesReport(DateTime startDate, DateTime endDate);
}