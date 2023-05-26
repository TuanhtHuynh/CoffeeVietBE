namespace CoffeeVietBE.Entity
{
  public class EntityBase
  {
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool Actived { get; set; }
  }
}