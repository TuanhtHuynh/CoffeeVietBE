using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Product;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Service
{
  public interface IProductService
  {
    Task<OkResponseModel<Pagination<ProductModel>>> GetAllAsync(ProductRequestModel request);
    Task<OkResponseModel<ProductModel>> GetAsync(int id);
    Task<OkResponseModel<ProductModel>> CreateAsync(EditProductModel entity);
    Task<BaseResponseModel> UpdateAsync(int id, EditProductModel entity);
    Task<BaseResponseModel> Delete(int id);
  }
}