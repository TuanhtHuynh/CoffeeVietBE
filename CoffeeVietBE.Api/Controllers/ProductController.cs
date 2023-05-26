using System.Threading.Tasks;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Product;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVietBE.Api.Controllers
{
  [ApiController]
  [Route("api/products")]
  public class ProductController : ControllerBase
  {
    protected readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
      _productService = productService;
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<Pagination<ProductModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] ProductRequestModel request)
    {
      var data = await _productService.GetAllAsync(request);
      return Ok(data);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(OkResponseModel<EditProductModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      return Ok(await _productService.GetAsync(id));
    }

    [HttpPost]
    [ProducesResponseType(typeof(OkResponseModel<EditProductModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAsync([FromBody] EditProductModel entity)
    {
      return Ok(await _productService.CreateAsync(entity));
    }

    [HttpPut]
    [Route("{product_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "product_id")] int Id, [FromBody] EditProductModel entity)
    {
      return Ok(await _productService.UpdateAsync(Id, entity));
    }

    [HttpDelete]
    [Route("{product_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "product_id")] int Id)
    {
      return Ok(await _productService.Delete(Id));
    }
  }
}