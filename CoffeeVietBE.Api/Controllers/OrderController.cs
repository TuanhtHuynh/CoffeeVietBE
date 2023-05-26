using System.Threading.Tasks;
using CoffeeVietBE.Model.Paginations;
using CoffeeVietBE.Model.Order;
using CoffeeVietBE.Model.Response;
using CoffeeVietBE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeVietBE.Api.Controllers
{
  [ApiController]
  [Route("api/Orders")]
  public class OrderController : ControllerBase
  {
    protected readonly IOrderService _OrderService;
    public OrderController(IOrderService OrderService)
    {
      _OrderService = OrderService;
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(typeof(OkResponseModel<Pagination<OrderModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromQuery] OrderRequestModel request)
    {
      var data = await _OrderService.GetAllAsync(request);
      return Ok(data);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(typeof(OkResponseModel<EditOrderModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      return Ok(await _OrderService.GetAsync(id));
    }

    [HttpPost]
    // [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(OkResponseModel<EditOrderModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateAsync([FromBody] EditOrderModel entity)
    {
      return Ok(await _OrderService.CreateAsync(entity));
    }

    [HttpPut]
    [Route("{order_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAsync([FromRoute(Name = "order_id")] int Id, [FromBody] EditOrderModel entity)
    {
      return Ok(await _OrderService.UpdateAsync(Id, entity));
    }

    [HttpDelete]
    [Route("{order_id:int}")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "order_id")] int Id)
    {
      return Ok(await _OrderService.Delete(Id));
    }
  }
}