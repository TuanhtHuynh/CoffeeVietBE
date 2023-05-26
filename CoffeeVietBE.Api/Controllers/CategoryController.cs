using System.Threading.Tasks;
using CoffeeVietBE.Model.Category;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVietBE.Api.Controllers
{
  [ApiController]
  [Route("api/categories")]
  public class CategoryController : ControllerBase
  {
    protected readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<Pagination<CategoryModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] CategoryRequestModel request)
    {
      var data = await _categoryService.GetAllAsync(request);
      return Ok(data);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(OkResponseModel<CategoryModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      return Ok(await _categoryService.GetAsync(1));
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAsync([FromBody] EditCategoryModel entity)
    {
      return Ok(await _categoryService.CreateAsync(entity));
    }

    [HttpPut]
    [Route("{category_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "category_id")] int Id, [FromBody] EditCategoryModel entity)
    {
      return Ok(await _categoryService.UpdateAsync(Id, entity));
    }

    [HttpDelete]
    [Route("{category_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "category_id")] int Id)
    {
      return Ok(await _categoryService.Delete(Id));
    }
  }
}