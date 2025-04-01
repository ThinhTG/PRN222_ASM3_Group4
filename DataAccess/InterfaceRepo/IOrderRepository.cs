using BusinessObject.Models;

namespace DataAccess.InterfaceRepo;
public interface IOrderRepository
{
    Task<Order> GetOrderById(int orderId);
    Task<IEnumerable<Order>> GetAllOrders();
    Task AddOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
    Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
    Task<decimal> GetTotalPriceByOrderId(int orderId);
}