using System.Linq.Expressions;
using AutoMapper;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Category;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Shared.Exceptions;

namespace CoffeeVietBE.Service
{
  public class CategoryService : ICategoryService
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
    }

    public async Task<OkResponseModel<Pagination<CategoryModel>>> GetAllAsync(CategoryRequestModel request)
    {
      Expression<Func<Category, bool>> predicate = null;
      if (!string.IsNullOrEmpty(request.Keyword))
      {
        predicate = (c) => !string.IsNullOrEmpty(request.Keyword) && c.Name.Contains(request.Keyword) && c.Actived;
      }
      var categories = await _unitOfWork.Category.GetPagingAll(predicate: predicate, order: GetCategorySort(request), include: null, request.PageIndex, request.PageSize);
      return new OkResponseModel<Pagination<CategoryModel>>(_mapper.Map<Pagination<CategoryModel>>(categories));
    }

    public async Task<OkResponseModel<CategoryModel>> GetAsync(int id)
    {
      var category = await _unitOfWork.Category.Get(c => c.CategoryId == 1);
      if (category == null)
      {
        throw new BadRequestException("the category is not exist");
      }
      return new OkResponseModel<CategoryModel>(_mapper.Map<CategoryModel>(category));
    }

    public async Task<BaseResponseModel> CreateAsync(EditCategoryModel entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        return new BaseResponseModel(System.Net.HttpStatusCode.BadRequest, "name is required");
      }
      var category = await _unitOfWork.Category.Get(c => c.Name == entity.Name);
      if (category != null)
        return new BaseResponseModel(System.Net.HttpStatusCode.BadRequest, "the category is existed");
      entity.CreatedDate = DateTime.Now;
      entity.UpdatedDate = DateTime.Now;

      var newCategory = _mapper.Map<Category>(entity);
      await _unitOfWork.Category.Create(newCategory);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> UpdateAsync(int id, EditCategoryModel entity)
    {
      var category = await _unitOfWork.Category.Get(c => c.CategoryId == id);
      if (category == null)
        throw new BadRequestException("the category is not exist");

      category.Name = entity.Name;
      category.Actived = entity.Actived;
      category.UpdatedDate = DateTime.Now;
      _unitOfWork.Category.Update(category);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> Delete(int id)
    {
      var category = await _unitOfWork.Category.Get(c => c.CategoryId == id);
      if (category == null)
      {
        throw new BadRequestException("the category is not existed");
      }
      await _unitOfWork.Category.Delete(id);
      await _unitOfWork.CompleteAsync();
      return new BaseResponseModel();
    }

    #region private method
    private static Func<IQueryable<Category>, IOrderedQueryable<Category>> GetCategorySort(CategoryRequestModel request)
    {
      return (query) =>
      {
        IOrderedQueryable<Category> categories = request.SortField?.ToLower() switch
        {
          "name_asc" => query.OrderBy(c => c.Name),
          "name_desc" => query.OrderByDescending(c => c.Name),
          _ => query.OrderByDescending(c => c.UpdatedDate)
        };
        return categories;
      };
    }
    #endregion
  }
}