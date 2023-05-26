using System.Net;
using System.Threading.Tasks;
using CoffeeVietBE.Model.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoffeeVietBE.Api.Middleware
{
  public class HandleExceptionMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<HandleExceptionMiddleware> _logger;

    public HandleExceptionMiddleware(RequestDelegate next, ILogger<HandleExceptionMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (System.Exception ex)
      {
        BaseResponseModel responseModel = new BaseResponseModel(HttpStatusCode.InternalServerError, ex.Message);
        if (ex is System.UnauthorizedAccessException)
        {
          responseModel.StatusCode = HttpStatusCode.Unauthorized;
        }
        else
        {
          context.Response.ContentType = "application/json";
          context.Response.StatusCode = (int)HttpStatusCode.OK;
        }


        await context.Response.WriteAsync(JsonConvert.SerializeObject(responseModel));
        _logger.LogError(ex, ex.Message);
      }
    }
  }
}