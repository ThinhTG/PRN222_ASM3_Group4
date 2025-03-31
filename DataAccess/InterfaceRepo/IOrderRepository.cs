using BusinessObject.Models;

namespace DataAccess.InterfaceRepo;
public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllOrders();
    Task<Order> GetOrderById(int orderId);
    Task AddOrder(Order order);
    Task UpdateOrder(Order order);
    Task DeleteOrder(int orderId);
    Task<IEnumerable<Order>> GetOrdersByDateRange(DateTime startDate, DateTime endDate);
}