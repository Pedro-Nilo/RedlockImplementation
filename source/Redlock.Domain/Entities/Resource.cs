using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Redlock.Domain.Entities;

public class Resource : Entity
{
    [BsonElement("name")]
    [JsonProperty(PropertyName = "Name")]
    public string Name { get; private set; }

    
    [BsonElement("inUse")]
    [JsonProperty(PropertyName = "InUse")]
    public bool InUse { get; private set; }

    public Resource(string name, bool inUse = false)
    {
        Name = name;
        InUse = inUse;
    }
}
