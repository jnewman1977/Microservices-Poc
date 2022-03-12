using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nitro.Msvc.Tenant.Access;

public class Tenant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TenantId { get; set; }

    public string Name { get; set; }
}