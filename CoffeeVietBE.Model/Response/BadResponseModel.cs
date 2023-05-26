using System.Net;

namespace CoffeeVietBE.Model.Response
{
  public class BadResponseModel : BaseResponseModel
  {
    public BadResponseModel() { }

    public BadResponseModel(string errorMessage) : base(HttpStatusCode.BadRequest, errorMessage)
    {
    }
  }
}