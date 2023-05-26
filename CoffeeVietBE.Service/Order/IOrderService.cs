using CoffeeVietBE.Model.Order;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Service
{
  public interface IOrderService
  {
    Task<OkResponseModel<Pagination<OrderModel>>> GetAllAsync(OrderRequestModel request);
    Task<OkResponseModel<OrderModel>> GetAsync(int id);
    Task<OkResponseModel<OrderModel>> CreateAsync(EditOrderModel entity);
    Task<BaseResponseModel> UpdateAsync(int id, EditOrderModel entity);
    Task<BaseResponseModel> Delete(int id);
  }
}