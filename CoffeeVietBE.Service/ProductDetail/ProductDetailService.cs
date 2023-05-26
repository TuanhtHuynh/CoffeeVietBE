using System.Linq.Expressions;
using AutoMapper;
using CoffeeVietBE.DataAccess.Repository;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.ProductDetail;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Shared.Exceptions;

namespace CoffeeVietBE.Service
{
  public class ProductDetailService : IProductDetailService
  {
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ProductDetailService(IMapper mapper, IUnitOfWork unitOfWork)
    {
      _mapper = mapper;
      _unitOfWork = unitOfWork;
    }

    public async Task<OkResponseModel<Pagination<ProductDetailModel>>> GetAllAsync(ProductDetailRequestModel request)
    {
      Expression<Func<ProductDetail, bool>> predicate = null;
      if (!string.IsNullOrEmpty(request.Keyword))
      {
        predicate = (c) => !string.IsNullOrEmpty(request.Keyword) && c.Size.Contains(request.Keyword) && c.Actived;
      }
      var productdetails = await _unitOfWork.ProductDetail.GetPagingAll(predicate: predicate, order: GetproductdetailSort(request),
      include: null, request.PageIndex, request.PageSize);
      return new OkResponseModel<Pagination<ProductDetailModel>>(_mapper.Map<Pagination<ProductDetailModel>>(productdetails));
    }

    public async Task<OkResponseModel<ProductDetailModel>> GetAsync(int id)
    {
      var productdetail = await _unitOfWork.ProductDetail.Get(c => c.ProductDetailId == id);
      if (productdetail == null)
      {
        throw new Exception("the productdetail is not exist");
      }
      return new OkResponseModel<ProductDetailModel>(_mapper.Map<ProductDetailModel>(productdetail));
    }

    public async Task<BaseResponseModel> CreateAsync(EditProductDetailModel entity)
    {
      if (string.IsNullOrEmpty(entity.Size))
      {
        throw new BadRequestException("name is required");
      }
      var productdetail = await _unitOfWork.ProductDetail.Get(c => c.Size == entity.Size);
      if (productdetail != null)
        throw new BadRequestException("the productdetail is existed");
      entity.CreatedDate = DateTime.Now;
      entity.UpdatedDate = DateTime.Now;

      var newproductdetail = _mapper.Map<ProductDetail>(entity);
      await _unitOfWork.ProductDetail.Create(newproductdetail);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> UpdateAsync(int id, EditProductDetailModel entity)
    {
      var productdetail = await _unitOfWork.ProductDetail.Get(c => c.ProductDetailId == id);
      if (productdetail == null)
        return new BaseResponseModel(System.Net.HttpStatusCode.InternalServerError, "the product detail is not exist");
      var duplicate = await _unitOfWork.ProductDetail.Get(c => c.Size == entity.Size);

      productdetail.Size = entity.Size;
      productdetail.Actived = entity.Actived;
      productdetail.UpdatedDate = DateTime.Now;
      _unitOfWork.ProductDetail.Update(productdetail);
      await _unitOfWork.CompleteAsync();

      return new BaseResponseModel();
    }

    public async Task<BaseResponseModel> Delete(int id)
    {
      var productdetail = await _unitOfWork.ProductDetail.Get(c => c.ProductDetailId == id);
      if (productdetail == null)
      {
        throw new Exception("the productdetail is not existed");
      }
      await _unitOfWork.ProductDetail.Delete(id);
      await _unitOfWork.CompleteAsync();
      return new BaseResponseModel();
    }

    #region private method
    private static Func<IQueryable<ProductDetail>, IOrderedQueryable<ProductDetail>> GetproductdetailSort(ProductDetailRequestModel request)
    {
      return (query) =>
      {
        IOrderedQueryable<ProductDetail> productdetails = request.SortField?.ToLower() switch
        {
          "name_desc" => query.OrderByDescending(c => c.Size),
          _ => query.OrderByDescending(c => c.UpdatedDate)
        };
        return productdetails;
      };
    }
    #endregion
  }
}