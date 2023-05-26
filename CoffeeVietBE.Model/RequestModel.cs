namespace CoffeeVietBE.Model
{
  public class RequestModel
  {
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 1;

    public string SortType { get; set; } = "asc";
    public string? SortField { get; set; }

    public string? Keyword { get; set; }
  }
}