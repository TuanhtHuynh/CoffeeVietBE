using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeVietBE.Entity
{
  public class ProductDetail : EntityBase
  {
    [Key]
    public int ProductDetailId { get; set; }

    [StringLength(5)]
    [Required]
    public string Size { get; set; }

    public int ProductId { get; set; }
  }
}