using System.Collections.Generic;

namespace CoffeeVietBE.Entity.Paginations
{
  public interface IPageList<T>
  {
    int From { get; }
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPages { get; }
    IList<T> Items { get; }
    bool HasPrevious { get; }
    bool HasNext { get; }
  }
}