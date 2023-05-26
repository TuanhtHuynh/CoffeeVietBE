using System.ComponentModel.DataAnnotations;

namespace CoffeeVietBE.Entity
{
  public class Category : EntityBase
  {
    [Key]
    public int CategoryId { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(256)]
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
  }
}