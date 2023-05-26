using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoffeeVietBE.Model
{
  public class AppUserModel : IdentityUser
  {
    [Required]
    public DateTime JoinedDate { get; set; }

    [Required]
    public string? UserType { get; set; }
  }
}