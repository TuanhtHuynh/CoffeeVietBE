using System.ComponentModel.DataAnnotations;
using CoffeeVietBE.Model.Product;
using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Models
{
  public class CategoryModel : ModelBase
  {
    [Key]
    public int CategoryId { get; set; }

    [Required]
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<ProductModel> Products { get; set; }
  }
}