using Newtonsoft.Json;

namespace AzureCosmos.Shared;
public abstract class BaseEntity
{
    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    [JsonProperty(PropertyName = "partitionKey")]
    public string PartitionKey { get; set; }

}
