using AutoMapper;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Entity.Paginations;
using CoffeeVietBE.Model.Category;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;

namespace CoffeeVietBE.Service.Mapping
{
  public class CategoryProfile : Profile
  {
    public CategoryProfile()
    {
      CreateMap<Category, CategoryModel>();
      CreateMap<EditCategoryModel, Category>();
      CreateMap<IPageList<Category>, Pagination<CategoryModel>>();
    }

  }
}