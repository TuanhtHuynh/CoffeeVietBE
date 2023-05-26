using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeVietBE.Model.Models;

namespace CoffeeVietBE.Model.Product
{
  public class ProductModel : ModelBase
  {
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }
    [Range(1000, int.MaxValue)]
    public int Price { get; set; }

    [Range(1000, int.MaxValue)]
    public int Discount { get; set; }

    public int CategoryId { get; set; }
    public int OrderDetailId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual CategoryModel Category { get; set; }
    public virtual List<ProductDetailModel> ProductDetails { get; set; }

    public string Image { get; set; }

    public int Amount { get; set; }
    public bool OnTop { get; set; }
  }
}