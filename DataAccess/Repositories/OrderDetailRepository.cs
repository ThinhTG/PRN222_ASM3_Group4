using BusinessObject;
using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly eStoreContext _context;

        public OrderDetailRepository(eStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int orderId)
        {
            return await _context
                .OrderDetails.Include(od => od.Product)
                .Where(od => od.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<OrderDetail> GetOrderDetail(int? orderId, int productId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(od =>
                od.OrderId == orderId && od.ProductId == productId
            );
        }

        public async Task<OrderDetail> GetOrderDetailById(int orderDetailId)
        {
            return await _context.OrderDetails.FirstOrDefaultAsync(od =>
                od.OrderDetailId == orderDetailId
            );
        }

        public async Task<OrderDetail> GetOrderDetailByProductIdWithNullOrderId(
            int productId,
            int memberId
        )
        {
            return await _context
                .OrderDetails.Include(od => od.Product)
                .FirstOrDefaultAsync(od =>
                    od.OrderId == null && od.ProductId == productId && od.MemberId == memberId
                );
        }

        public async Task AddOrderDetail(OrderDetail orderDetail)
        {
            await _context.OrderDetails.AddAsync(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderDetail(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderDetail(int orderId, int productId)
        {
            var orderDetail = await GetOrderDetail(orderId, productId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderDetailById(int orderDetailId)
        {
            var orderDetail = await GetOrderDetailById(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByMemberId(int memberId)
        {
            return await _context
                .OrderDetails.Include(od => od.Product)
                .Where(od => od.MemberId == memberId)
                .ToListAsync();
        }
    }
}
