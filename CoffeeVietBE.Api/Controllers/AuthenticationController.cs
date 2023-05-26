using System.Threading.Tasks;
using CoffeeVietBE.Model.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoffeeVietBE.Service;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {
    private readonly IUserService _userService;

    public AuthenticationController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(typeof(BaseResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
      return Ok(await _userService.Register(model));
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(typeof(AuthorizedResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
      return Ok(await _userService.Login(model));
    }
  }
}