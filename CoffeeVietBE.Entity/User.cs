using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CoffeeVietBE.Entity
{
  public class User : IdentityUser
  {
    [Required]
    public DateTime? JoinedDate { get; set; }

    [Required]
    public string? UserType { get; set; }
  }
}