using System.ComponentModel.DataAnnotations;

namespace CoffeeVietBE.Model.Request
{
  public class RegisterModel
  {
    [Required]
    public string UserName { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public DateTime? JoinedDate { get; set; }
  }
}