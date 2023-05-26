using System.Net;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Response
{
  public class BaseResponseModel
  {
    public BaseResponseModel()
    {
      StatusCode = HttpStatusCode.OK;
    }
    public BaseResponseModel(
      HttpStatusCode statusCode, string errorMessage)
    {
      StatusCode = statusCode;
      ErrorMessage = errorMessage;
    }

    [JsonProperty("status")]
    public bool Status
    {
      get
      {
        return string.IsNullOrEmpty(ErrorMessage);
      }
    }

    [JsonProperty("code")]
    public HttpStatusCode StatusCode { get; set; }

    [JsonProperty("error")]
    public string ErrorMessage { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
  }

}