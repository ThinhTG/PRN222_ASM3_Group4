using AutoMapper;
using BusinessObject.Models;

namespace DataAccess.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, Order>().ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetail, OrderDetail>();
        }   
    }
}
