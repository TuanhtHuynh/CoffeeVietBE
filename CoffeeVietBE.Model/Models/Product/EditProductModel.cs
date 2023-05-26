using CoffeeVietBE.Model.Models;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Product
{
  public class EditProductModel : ModelBase
  {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("price")]
    public int Price { get; set; }

    [JsonProperty("dis_count")]
    public int Discount { get; set; }

    [JsonProperty("category_id")]
    public int CategoryId { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; }

    [JsonProperty("amount")]
    public int Amount { get; set; }

    [JsonProperty("on_top")]
    public bool OnTop { get; set; }
  }
}