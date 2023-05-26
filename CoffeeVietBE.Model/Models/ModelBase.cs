using Newtonsoft.Json;

namespace CoffeeVietBE.Model.Models
{
  public class ModelBase
  {
    [JsonProperty("created_date")]

    public DateTime CreatedDate { get; set; }

    [JsonProperty("updated_date")]
    public DateTime? UpdatedDate { get; set; }

    [JsonProperty("actived")]
    public bool Actived { get; set; }
  }
}