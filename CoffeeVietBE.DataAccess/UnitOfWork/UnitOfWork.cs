using CoffeeVietBE.DataAccess.Data;
using CoffeeVietBE.DataAccess.Repository;
using CoffeeVietBE.Entity;

namespace CoffeeVietBE.DataAccess.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
      _context = context;
    }

    public IGenericRepository<Category> Category
    {
      get
      {
        return new GenericRepository<Category>(_context);
      }
    }
    public IGenericRepository<ProductDetail> ProductDetail
    {
      get
      {
        return new GenericRepository<ProductDetail>(_context);
      }
    }
    public IGenericRepository<Product> Product
    {
      get
      {
        return new GenericRepository<Product>(_context);
      }
    }
    public IGenericRepository<Order> Order
    {
      get
      {
        return new GenericRepository<Order>(_context);
      }
    }

    public IGenericRepository<User> User
    {
      get
      {
        return new GenericRepository<User>(_context);
      }
    }

    public async Task CompleteAsync()
    {
      await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}