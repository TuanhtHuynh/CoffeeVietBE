using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeVietBE.Model.Product;

namespace CoffeeVietBE.Model.Models
{
  public class ProductDetailModel : ModelBase
  {
    [Key]
    public int ProductDetailId { get; set; }

    [StringLength(5)]
    [Required]
    public string Size { get; set; }
  }
}