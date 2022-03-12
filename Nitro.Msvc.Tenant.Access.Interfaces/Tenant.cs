using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Nitro.Msvc.Tenant.Access.Interfaces;

namespace Nitro.Msvc.Tenant.Access;

public class Tenant : ITenant
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string TenantId { get; set; }

    public string Name { get; set; }
}