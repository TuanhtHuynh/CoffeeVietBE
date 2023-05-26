using AutoMapper;
using CoffeeVietBE.Service.Mapping;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class AutoMapperExtensions
  {
    public static void AddAutoMapper(this IServiceCollection services)
    {
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new CategoryProfile());
        mc.AddProfile(new ProductProfile());
        mc.AddProfile(new ProductDetailProfile());
        mc.AddProfile(new OrderProfile());
        mc.AddProfile(new UserProfile());
      });
      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);
    }
  }
}