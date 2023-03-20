using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Redlock.Domain.Entities;

public abstract class Entity
{
    [BsonId]
    [BsonIgnoreIfDefault]
    [BsonRepresentation(BsonType.ObjectId)]
    [Key]
    [JsonProperty(PropertyName = "Id")]
    public string Id { get; private set; }

    [BsonElement("createdAt")]
    [JsonProperty(PropertyName = "CreatedAt")]
    public string CreatedAt { get; private set; }

    [BsonElement("updatedAt")]
    [JsonProperty(PropertyName = "UpdatedAt")]
    public string UpdatedAt { get; private set; }

    public Entity()
    {
        Id = ObjectId.GenerateNewId().ToString();
        CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
