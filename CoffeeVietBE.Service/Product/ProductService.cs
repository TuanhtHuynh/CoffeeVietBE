using System.Linq.Expressions;
using AutoMapper;
using CoffeeVietBE.DataAccess.Repository;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Product;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CoffeeVietBE.Service
{
  public class ProductService : IProductService
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
    }

    public async Task<OkResponseModel<Pagination<ProductModel>>> GetAllAsync(ProductRequestModel request)
    {
      Expression<Func<Product, bool>> predicate = null;
      if (!string.IsNullOrEmpty(request.Keyword))
      {
        predicate = (c) => !string.IsNullOrEmpty(request.Keyword) && c.Name.Contains(request.Keyword) && c.Actived;
      }
      var Products = await _unitOfWork.Product.GetPagingAll(predicate: predicate, order: GetProductSort(request), GetInclude(), request.PageIndex, request.PageSize);
      return new OkResponseModel<Pagination<ProductModel>>(_mapper.Map<Pagination<ProductModel>>(Products));
    }



    public async Task<OkResponseModel<ProductModel>> GetAsync(int id)
    {
      var Product = await _unitOfWork.Product.Get(c => c.ProductId == id);
      if (Product == null)
      {
        throw new BadRequestException("the Product is not exist");
      }
      return new OkResponseModel<ProductModel>(_mapper.Map<ProductModel>(Product));
    }

    public async Task<OkResponseModel<ProductModel>> CreateAsync(EditProductModel entity)
    {
      if (string.IsNullOrEmpty(entity.Name))
      {
        throw new BadRequestException("name is required");
      }
      var Product = await _unitOfWork.Product.Get(c => c.Name.ToLower() == entity.Name.ToLower());
      if (Product != null)
        throw new BadRequestException("the Product is existed");
      entity.CreatedDate = DateTime.Now;
      entity.UpdatedDate = DateTime.Now;

      var newProduct = _mapper.Map<Product>(entity);
      await _unitOfWork.Product.Create(newProduct);
      await _unitOfWork.CompleteAsync();

      var Id = newProduct.ProductId;

      var productInserted = await _unitOfWork.Product.Get(c => c.ProductId == Id);

      if (productInserted == null)
      {
        throw new BadRequestException("the Product is not exist");
      }
      return new OkResponseModel<ProductModel>(_mapper.Map<ProductModel>(productInserted));

    }

    public async Task<BaseResponseModel> UpdateAsync(int id, EditProductModel entity)
    {
      var Product = await _unitOfWork.Product.Get(c => c.ProductId == id);
      if (Product == null)
        throw new BadRequestException("the product detail is not exist");
      var duplicate = await _unitOfWork.Product.Get(c => c.Name == entity.Name);

      Product.Name = entity.Name;
      Product.Actived = entity.Actived;
      Product.UpdatedDate = DateTime.Now;
      _unitOfWork.Product.Update(Product);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> Delete(int id)
    {
      var Product = await _unitOfWork.Product.Get(c => c.ProductId == id);
      if (Product == null)
      {
        throw new BadRequestException("the Product is not existed");
      }
      await _unitOfWork.Product.Delete(id);
      await _unitOfWork.CompleteAsync();
      return new BaseResponseModel();
    }

    #region [private method]
    private Func<IQueryable<Product>, IIncludableQueryable<Product, object>> GetInclude()
    {
      return (query) => query.Include(p => p.Category).Include(p => p.ProductDetails);
    }
    private Func<IQueryable<Product>, IOrderedQueryable<Product>> GetProductSort(ProductRequestModel request)
    {
      return (query) =>
      {
        IOrderedQueryable<Product> Products = request.SortField?.ToLower() switch
        {
          "name_desc" => query.OrderByDescending(c => c.Name),
          _ => query.OrderByDescending(c => c.UpdatedDate)
        };
        return Products;
      };
    }
    #endregion
  }
}