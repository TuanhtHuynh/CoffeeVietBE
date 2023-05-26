using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.ProductDetail;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Service
{
  public interface IProductDetailService
  {
    Task<OkResponseModel<Pagination<ProductDetailModel>>> GetAllAsync(ProductDetailRequestModel request);
    Task<OkResponseModel<ProductDetailModel>> GetAsync(int id);
    Task<BaseResponseModel> CreateAsync(EditProductDetailModel entity);
    Task<BaseResponseModel> UpdateAsync(int id, EditProductDetailModel entity);
    Task<BaseResponseModel> Delete(int id);
  }
}