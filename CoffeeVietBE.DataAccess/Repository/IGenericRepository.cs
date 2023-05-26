using System.Linq.Expressions;
using CoffeeVietBE.Entity.Paginations;
using Microsoft.EntityFrameworkCore.Query;

namespace CoffeeVietBE.DataAccess.Repository
{
  public interface IGenericRepository<T> where T : class
  {
    Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    Task<IPageList<T>> GetPagingAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int pageIndex = 1, int pageSize = 15, int from = 1);
    Task<int> Count(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, CancellationToken cancel = default);

    Task<IEnumerable<T>> GetTop(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int numberOfItem = 0);
    Task<T> Get(Expression<Func<T, bool>> predicate);
    Task Create(T entity);
    void Update(T entity);
    Task Delete(int id);
  }
}