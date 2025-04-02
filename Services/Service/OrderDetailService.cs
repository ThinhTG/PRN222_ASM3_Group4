using AutoMapper;
using BusinessObject.Models;
using DataAccess.InterfaceRepo;
using Services.InterfaceService;

namespace Services.Service;

public class OrderDetailService : IOrderDetailService
{
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IMapper _mapper;

    public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
    {
        _orderDetailRepository = orderDetailRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderId)
    {
        return await _orderDetailRepository.GetOrderDetailsByOrderId(orderId);
    }

    public async Task<OrderDetail> GetOrderDetailByProductIdWithNullOrderId(
        int productId,
        int memberId
    )
    {
        // You'll need to add this method to your repository too
        return await _orderDetailRepository.GetOrderDetailByProductIdWithNullOrderId(
            productId,
            memberId
        );
    }

    public async Task<OrderDetail> GetOrderDetail(int orderId, int productId)
    {
        return await _orderDetailRepository.GetOrderDetail(orderId, productId);
    }

    public async Task CreateOrderDetail(OrderDetail orderDetail)
    {
        await _orderDetailRepository.AddOrderDetail(orderDetail);
    }

    public async Task UpdateOrderDetail(OrderDetail orderDetail)
    {
        var existingDetail = await _orderDetailRepository.GetOrderDetailById(orderDetail.OrderDetailId);
        if (existingDetail != null)
        {
            _mapper.Map(orderDetail, existingDetail);
            await _orderDetailRepository.UpdateOrderDetail(existingDetail);
        }
    }

    public async Task DeleteOrderDetail(int orderId, int productId)
    {
        await _orderDetailRepository.DeleteOrderDetail(orderId, productId);
    }

    public async Task DeleteOrderDetailById(int orderDetailId)
    {
        await _orderDetailRepository.DeleteOrderDetailById(orderDetailId);
    }

    public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByMemberId(int memberId)
    {
        return await _orderDetailRepository.GetOrderDetailsByMemberId(memberId);
    }
}
