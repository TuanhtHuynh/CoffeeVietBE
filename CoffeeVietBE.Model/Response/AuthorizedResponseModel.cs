
using System.Net;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Response
{
  public class AuthorizedResponseModel : BaseResponseModel
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonProperty("token_type")]
    public string TokenType => "bearer";

    [JsonProperty("expire_in")]
    public DateTime ExpireIn { get; set; }

    public AuthorizedResponseModel()
    {

    }

    public AuthorizedResponseModel(string errorMessage)
    {
      this.ErrorMessage = errorMessage;
      this.StatusCode = HttpStatusCode.Unauthorized;
    }

    public AuthorizedResponseModel(string accessToken, string refreshToken, DateTime expire)
    {
      AccessToken = accessToken;
      RefreshToken = refreshToken;
      ExpireIn = expire;
      StatusCode = HttpStatusCode.OK;
    }
  }
}