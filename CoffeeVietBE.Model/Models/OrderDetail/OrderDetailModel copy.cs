
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.Product;

namespace CoffeeVietBE.Model.OrderDetail
{
  public class OrderDetailModel : ModelBase
  {
    [Key]
    public int OrderDetailId { get; set; }
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }
    public ProductModel Product { get; set; }

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