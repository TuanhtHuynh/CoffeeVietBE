using Microsoft.AspNetCore.Mvc.Filters;

namespace Filter
{
  public class ExceptionFilter : IExceptionFilter
  {
    public void OnException(ExceptionContext context)
    {
      throw new System.NotImplementedException();
    }
  }
}