using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Nitro.Msvc.User.Access;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string UserId { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
}