using CoffeeVietBE.DataAccess.Repository;
using CoffeeVietBE.Entity;

namespace CoffeeVietBE.DataAccess.UnitOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    IGenericRepository<Category> Category { get; }
    IGenericRepository<ProductDetail> ProductDetail { get; }
    IGenericRepository<Product> Product { get; }
    IGenericRepository<Order> Order { get; }
    IGenericRepository<User> User { get; }
    Task CompleteAsync();
  }
}