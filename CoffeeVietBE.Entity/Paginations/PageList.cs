namespace CoffeeVietBE.Entity.Paginations
{
  public class PageList<T> : IPageList<T> where T : class
  {
    public PageList(int from, int pageIndex, int pageSize, int totalCount, int totalPages, IList<T> items)
    {
      From = from;
      PageIndex = pageIndex;
      PageSize = pageSize;
      TotalCount = totalCount;
      TotalPages = totalPages;
      Items = items;
    }

    public int From { get; }

    public int PageIndex { get; }

    public int PageSize { get; }

    public int TotalCount { get; }

    public int TotalPages { get; }

    public IList<T> Items { get; }

    public bool HasPrevious => PageIndex > From;
    public bool HasNext => PageIndex < TotalPages;
  }
}