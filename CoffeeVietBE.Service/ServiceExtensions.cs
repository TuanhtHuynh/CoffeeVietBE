using CoffeeVietBE.DataAccess.Data;
using CoffeeVietBE.DataAccess.Repository;
using CoffeeVietBE.DataAccess.UnitOfWork;
using CoffeeVietBE.Entity;
using CoffeeVietBE.Model;
using CoffeeVietBE.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
  public static class ServiceExtensions
  {
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
      services.AddScoped<ICategoryService, CategoryService>();
      services.AddScoped<IGenericRepository<ProductDetail>, GenericRepository<ProductDetail>>();
      services.AddScoped<IProductDetailService, ProductDetailService>();
      services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
      services.AddScoped<IProductService, ProductService>();
      services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IUserService, UserService>();

      services.AddAutoMapper();

      //Identity
      services.AddIdentityCore<User>()
      .AddRoles<IdentityRole>()
       .AddEntityFrameworkStores<ApplicationDbContext>()
       .AddDefaultTokenProviders();
    }
  }
}