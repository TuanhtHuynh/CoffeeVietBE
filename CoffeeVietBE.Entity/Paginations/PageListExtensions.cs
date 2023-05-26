using Microsoft.EntityFrameworkCore;
using System;
namespace CoffeeVietBE.Entity.Paginations
{
  public static class PageListExtensions
  {
    public static async Task<IPageList<T>> ToPageListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, int from = 1) where T : class
    {
      if (from > pageIndex)
      {
        pageIndex = from;
      }
      int totalCount = await source.CountAsync();
      var items = await source.Skip((pageIndex - from) * pageSize).Take(pageSize).ToListAsync();
      int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
      return new PageList<T>(from, pageIndex, pageSize, totalCount, totalPages, items);
    }
  }
}