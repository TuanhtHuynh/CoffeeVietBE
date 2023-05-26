using System.Threading.Tasks;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.ProductDetail;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVietBE.Api.Controllers
{
  [ApiController]
  [Route("api/productdetails")]
  public class ProductDetailController : ControllerBase
  {
    protected readonly IProductDetailService _productDetailService;
    public ProductDetailController(IProductDetailService productService)
    {
      _productDetailService = productService;
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<Pagination<ProductDetailModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] ProductDetailRequestModel request)
    {
      var data = await _productDetailService.GetAllAsync(request);
      return Ok(data);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(OkResponseModel<ProductDetailModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      return Ok(await _productDetailService.GetAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAsync([FromBody] EditProductDetailModel entity)
    {
      return Ok(await _productDetailService.CreateAsync(entity));
    }

    [HttpPut]
    [Route("{productdetail_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "productdetail_id")] int Id, [FromBody] EditProductDetailModel entity)
    {
      return Ok(await _productDetailService.UpdateAsync(Id, entity));
    }

    [HttpDelete]
    [Route("{productdetail_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "productdetail_id")] int Id)
    {
      return Ok(await _productDetailService.Delete(Id));
    }
  }
}