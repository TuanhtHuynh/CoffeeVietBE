using System.Linq.Expressions;
using CoffeeVietBE.DataAccess.Data;
using CoffeeVietBE.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeVietBE.DataAccess.Repository
{
  public class CategoryRepository : GenericRepository<Product>
  {
    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<Category> Delete(int id)
    // {
    //   var category = await _dbset.FindAsync(id);
    //   if (category != null)
    //   {
    //     _dbset.Remove(category);
    //   }
    //   return new Category();
    // }
  }
}