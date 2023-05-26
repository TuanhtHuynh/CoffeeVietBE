using System.ComponentModel.DataAnnotations;

namespace CoffeeVietBE.Model.Request
{
  public class LoginModel
  {
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
  }
}