using System.ComponentModel.DataAnnotations;
using CoffeeVietBE.Model.Models;
using CoffeeVietBE.Model.OrderDetail;

namespace CoffeeVietBE.Model.Order
{
  public class EditOrderModel : ModelBase
  {
    [Range(1000, int.MaxValue)]
    public int TotalPrice { get; set; }
    public int Amount { get; set; }
    public int Refund { get; set; }
  }
}