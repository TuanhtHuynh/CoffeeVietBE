using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Category;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Service
{
  public interface ICategoryService
  {
    Task<OkResponseModel<Pagination<CategoryModel>>> GetAllAsync(CategoryRequestModel request);
    Task<OkResponseModel<CategoryModel>> GetAsync(int id);
    Task<BaseResponseModel> CreateAsync(EditCategoryModel entity);
    Task<BaseResponseModel> UpdateAsync(int id, EditCategoryModel entity);
    Task<BaseResponseModel> Delete(int id);
  }
}