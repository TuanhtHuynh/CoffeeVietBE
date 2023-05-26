using System.Linq.Expressions;
using AutoMapper;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Order;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CoffeeVietBE.Service
{
  public class OrderService : IOrderService
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
    }

    public async Task<OkResponseModel<Pagination<OrderModel>>> GetAllAsync(OrderRequestModel request)
    {
      Expression<Func<Order, bool>> predicate = null;
      if (!string.IsNullOrEmpty(request.Keyword))
      {
        predicate = (c) => !string.IsNullOrEmpty(request.Keyword) && c.OrderId == Convert.ToInt32(request.Keyword) && c.Actived;
      }
      var Orders = await _unitOfWork.Order.GetPagingAll(predicate: predicate, order: GetOrderSort(request), GetInclude(), request.PageIndex, request.PageSize);
      return new OkResponseModel<Pagination<OrderModel>>(_mapper.Map<Pagination<OrderModel>>(Orders));
    }



    public async Task<OkResponseModel<OrderModel>> GetAsync(int id)
    {
      var Order = await _unitOfWork.Order.Get(c => c.OrderId == id);
      if (Order == null)
      {
        throw new BadRequestException("the Order is not exist");
      }
      return new OkResponseModel<OrderModel>(_mapper.Map<OrderModel>(Order));
    }

    public async Task<OkResponseModel<OrderModel>> CreateAsync(EditOrderModel entity)
    {
      entity.CreatedDate = DateTime.Now;
      entity.UpdatedDate = DateTime.Now;

      var newOrder = _mapper.Map<Order>(entity);
      await _unitOfWork.Order.Create(newOrder);
      await _unitOfWork.CompleteAsync();

      var Id = newOrder.OrderId;

      var OrderInserted = await _unitOfWork.Order.Get(c => c.OrderId == Id);

      if (OrderInserted == null)
      {
        throw new BadRequestException("the Order is not exist");
      }
      return new OkResponseModel<OrderModel>(_mapper.Map<OrderModel>(OrderInserted));
    }

    public async Task<BaseResponseModel> UpdateAsync(int id, EditOrderModel entity)
    {
      var Order = await _unitOfWork.Order.Get(c => c.OrderId == id);
      if (Order == null)
        return new BaseResponseModel(System.Net.HttpStatusCode.InternalServerError, "the Order detail is not exist");

      Order.Amount = Order.Amount;
      Order.TotalPrice = Order.TotalPrice;
      Order.Actived = entity.Actived;
      Order.UpdatedDate = DateTime.Now;
      _unitOfWork.Order.Update(Order);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> Delete(int id)
    {
      var Order = await _unitOfWork.Order.Get(c => c.OrderId == id);
      if (Order == null)
      {
        throw new BadRequestException("the Order is not existed");
      }
      await _unitOfWork.Order.Delete(id);
      await _unitOfWork.CompleteAsync();
      return new BaseResponseModel();
    }

    #region [private method]
    private Func<IQueryable<Order>, IIncludableQueryable<Order, object>> GetInclude()
    {
      return (query) => query.Include(p => p.OrderDetails);
    }
    private Func<IQueryable<Order>, IOrderedQueryable<Order>> GetOrderSort(OrderRequestModel request)
    {
      return (query) =>
      {
        IOrderedQueryable<Order> Orders = request.SortField?.ToLower() switch
        {
          _ => query.OrderByDescending(c => c.UpdatedDate)
        };
        return Orders;
      };
    }
    #endregion
  }
}