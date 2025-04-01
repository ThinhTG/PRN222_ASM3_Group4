using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.DBContext;
using DataAccess.InterfaceRepo;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly eStoreContext _context;

        public OrderRepository(eStoreContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            // Eager loading cả Member và OrderDetails, bao gồm Product trong OrderDetails
            var order = await _context
                .Orders.Include(o => o.Member)
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product) // Thêm dòng này để tải Product
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                Console.WriteLine($"Order with ID {orderId} not found in database.");
            }
            else if (order.OrderDetails == null || !order.OrderDetails.Any())
            {
                Console.WriteLine($"Order {orderId} loaded but has no OrderDetails.");
            }
            else
            {
                Console.WriteLine(
                    $"Order {orderId} loaded with {order.OrderDetails.Count} OrderDetails."
                );
            }

            return order;
        }

        // Các phương thức khác giữ nguyên
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.Member)
                .Include(o => o.OrderDetails) // Load OrderDetails để tính TotalPrice
            return await _context.Orders.Include(o => o.Member).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersByMemberId(int memberId)
        {
            return await _context
                .Orders.Include(o => o.Member)
                .Where(o => o.MemberId == memberId)
                .ToListAsync();
        }


        public async Task AddOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddOrderReturn(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await GetOrderById(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByDateRange(
            DateTime startDate,
            DateTime endDate
        )
        {
            return await _context.Orders
                .Include(o => o.OrderDetails) // Load OrderDetails
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
            return await _context
                .Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                .ToListAsync();
        }


        public async Task<decimal> GetTotalPriceByOrderId(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
            {
                return 0;
            }

            return order.TotalPrice;
        }


    }
}
