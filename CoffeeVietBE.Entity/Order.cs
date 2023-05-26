using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeVietBE.Entity
{
  public class Order : EntityBase
  {
    [ForeignKey("Order")]
    [Required]
    public int OrderId { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }

    [Required]
    public User Customer { get; set; }
    [Required]
    public User Staff { get; set; }
   
    [Range(1000, int.MaxValue)]
    public int TotalPrice { get; set; }
    public int Amount { get; set; }

    public int Refund { get; set; }
  }
}