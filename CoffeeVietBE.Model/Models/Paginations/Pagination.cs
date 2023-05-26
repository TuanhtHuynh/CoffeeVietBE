using System.Collections;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Paginations
{
  public class Pagination<T>
  {
    [JsonIgnore]
    public int From { get; set; }

    [JsonProperty("page", NullValueHandling = NullValueHandling.Ignore)]
    public int PageIndex { get; set; }

    [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
    public int PageSize { get; set; }

    [JsonProperty("total_count", NullValueHandling = NullValueHandling.Ignore)]
    public int TotalCount { get; set; }

    [JsonProperty("total_pages", NullValueHandling = NullValueHandling.Ignore)]
    public int TotalPages { get; set; }

    [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<T> Items { get; set; }

    [JsonProperty("has_previous_page", NullValueHandling = NullValueHandling.Ignore)]
    public bool HasPrevious { get; set; }

    [JsonProperty("has_next_page", NullValueHandling = NullValueHandling.Ignore)]
    public bool HasNext { get; set; }
  }
}