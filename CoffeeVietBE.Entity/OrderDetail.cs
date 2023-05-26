
using System.ComponentModel.DataAnnotations;

namespace CoffeeVietBE.Entity
{
  public class OrderDetail : EntityBase
  {
    [Key]
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public Product Product { get; set; }

    [Range(1, int.MaxValue)]
    public int TotalAmount { get; set; }

    [Required, Range(2000, int.MaxValue)]
    public int Discount { get; set; }

    [Required, Range(1000, int.MaxValue)]
    public int TotalPrice { get; set; }

    [Required]
    public string Unit { get; set; }
  }
}