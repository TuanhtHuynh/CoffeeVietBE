using System.ComponentModel.DataAnnotations.Schema;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Product;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.ProductDetail
{
  public class EditProductDetailModel : ModelBase
  {
    [JsonProperty("size")]
    public string Size { get; set; }

    [JsonProperty("product_id")]
    public int ProductId { get; set; }
  }
}