using AutoMapper;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Entity.Paginations;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.ProductDetail;

namespace CoffeeVietBE.Service.Mapping
{
  public class ProductDetailProfile : Profile
  {
    public ProductDetailProfile()
    {
      CreateMap<ProductDetail, ProductDetailModel>();
      CreateMap<EditProductDetailModel, ProductDetail>();
      CreateMap<IPageList<ProductDetail>, Pagination<ProductDetailModel>>();
    }
  }
}