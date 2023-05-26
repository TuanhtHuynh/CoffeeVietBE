using System.Net;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Response
{
  public class OkResponseModel<T> : BaseResponseModel
  {
    [JsonProperty("data")]
    public T Data { get; set; }
    public OkResponseModel()
    {
      Data = default;
    }
    public OkResponseModel(T data)
    {
      Data = data;
    }

    public OkResponseModel(T data, string message)
    {
      Data = data;
      Message = message;
      StatusCode = HttpStatusCode.OK;
    }
  }
}