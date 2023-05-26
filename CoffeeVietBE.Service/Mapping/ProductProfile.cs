using AutoMapper;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Entity.Paginations;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Product;

namespace CoffeeVietBE.Service.Mapping
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<Product, ProductModel>();
      CreateMap<EditProductModel, Product>();
      CreateMap<IPageList<Product>, Pagination<ProductModel>>();
    }
  }
}