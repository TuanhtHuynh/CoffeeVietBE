using CoffeeVietBE.Model.Models;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Category
{
  public class EditCategoryModel : ModelBase
  {
    [JsonProperty("name")]
    public string Name { get; set; }
  }
}