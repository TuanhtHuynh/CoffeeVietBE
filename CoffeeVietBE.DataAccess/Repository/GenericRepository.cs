using System.Linq.Expressions;
using CoffeeVietBE.DataAccess.Data;
using CoffeeVietBE.Entity.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CoffeeVietBE.DataAccess.Repository
{
  public class GenericRepository<T> : IGenericRepository<T> where T : class
  {
    protected ApplicationDbContext _context;
    protected DbSet<T> _dbset;

    public GenericRepository(ApplicationDbContext context)
    {
      _context = context;
      _dbset = _context.Set<T>();
    }

    public async Task Create(T entity)
    {
      await _dbset.AddAsync(entity);
    }
    public void Update(T entity)
    {
      _dbset.Update(entity);
    }

    public virtual async Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
      return await Query(predicate, order, include).ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> predicate)
    {
      return await Query(predicate).FirstOrDefaultAsync();
    }

    public async Task<IPageList<T>> GetPagingAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int pageIndex = 1, int pageSize = 15, int from = 1)
    {
      return await Query(predicate, order, include).ToPageListAsync(pageIndex, pageSize, from);
    }

    public async Task<int> Count(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, CancellationToken cancel = default)
   => await Query(predicate, order, include).CountAsync();

    public async Task<IEnumerable<T>> GetTop(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int numberOfItem = 0)
    => (await FindAll(predicate, order, include)).Take(numberOfItem);

    private async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IOrderedQueryable<T>> order, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
    {
      return await Query(predicate, order, include).ToListAsync();
    }

    public async Task Delete(int id)
    {
      var data = await _dbset.FindAsync(id);
      if (data != null)
      {
        _dbset.Remove(data);
      }
    }

    private IQueryable<T> Query(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? order = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
      IQueryable<T> query = _dbset;
      if (include != null)
      {
        query = include(query);
      }
      if (predicate != null)
      {
        query = query.Where(predicate);
      }
      if (order != null)
      {
        query = order(query);
      }
      return query.AsNoTracking();
    }
  }
}