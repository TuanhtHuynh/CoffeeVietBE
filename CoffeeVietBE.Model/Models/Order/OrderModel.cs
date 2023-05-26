using System.ComponentModel.DataAnnotations;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.OrderDetail;

namespace CoffeeVietBE.Model.Order
{
  public class OrderModel : ModelBase
  {
    [Key]
    public int OrderId { get; set; }

    public ICollection<OrderDetailModel> OrderDetailsModel { get; set; }
    [Range(1000, int.MaxValue)]
    public AppUserModel Customer { get; set; }
    public AppUserModel Staff { get; set; }
    public int TotalPrice { get; set; }
    public int Amount { get; set; }
    public int Refund { get; set; }
  }
}