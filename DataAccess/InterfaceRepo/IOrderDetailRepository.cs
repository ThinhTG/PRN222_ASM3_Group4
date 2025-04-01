using BusinessObject.Models;

namespace DataAccess.InterfaceRepo;

public interface IOrderDetailRepository
{
    Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId);
    Task<OrderDetail> GetOrderDetail(int? orderId, int productId);
    Task AddOrderDetail(OrderDetail orderDetail);
    Task UpdateOrderDetail(OrderDetail orderDetail);
    Task DeleteOrderDetail(int orderId, int productId);
    Task<OrderDetail> GetOrderDetailByProductIdWithNullOrderId(int productId, int memberId);
    Task<IEnumerable<OrderDetail>> GetOrderDetailsByMemberId(int memberId);
    Task DeleteOrderDetailById(int orderDetailId);
    Task<OrderDetail> GetOrderDetailById(int orderDetailId);
}
