using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeVietBE.Entity
{
  public class Product : EntityBase
  {
    public int ProductId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(256)]
    public string Name { get; set; }
    [Range(1000, int.MaxValue)]
    public int Price { get; set; }

    [Range(1000, int.MaxValue)]
    public int Discount { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public virtual Category Category { get; set; }

    public virtual List<ProductDetail> ProductDetails { get; set; }

    public string Image { get; set; }

    public int Amount { get; set; }
    public bool OnTop { get; set; }
  }
}