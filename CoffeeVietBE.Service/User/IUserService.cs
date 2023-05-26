using CoffeeVietBE.Model.Request;
using CoffeeVietBE.Model.Response;

namespace CoffeeVietBE.Service
{
  public interface IUserService
  {
    Task<AuthorizedResponseModel> Login(LoginModel user);
    Task<BaseResponseModel> Register(RegisterModel user);
  }
}