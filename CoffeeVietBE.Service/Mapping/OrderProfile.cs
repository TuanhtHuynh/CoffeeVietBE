using AutoMapper;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Entity.Paginations;
using CoffeeVietBE.Model.Order;
using CoffeeVietBE.Model.Paginations;

namespace CoffeeVietBE.Service.Mapping
{
  public class OrderProfile : Profile
  {
    public OrderProfile()
    {
      CreateMap<Order, OrderModel>();
      CreateMap<EditOrderModel, Order>();
      CreateMap<IPageList<Order>, Pagination<OrderModel>>();
    }

  }
}