using Microsoft.AspNetCore.Identity;

namespace CoffeeVietBE.Model
{
  public class ApplicationUserModelBase : IdentityUser
  {
    public virtual ICollection<IdentityRole>? UserRoles { get; set; }

    public DateTime JoinedDate { get; set; }
  }
}