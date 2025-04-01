using BusinessObject.Models;

namespace Services.InterfaceService;

public interface IOrderDetailService
{
    Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId);
    Task<OrderDetail> GetOrderDetail(int orderId, int productId);
    Task CreateOrderDetail(OrderDetail orderDetail);
    Task UpdateOrderDetail(OrderDetail orderDetail);
    Task DeleteOrderDetail(int orderId, int productId);
}