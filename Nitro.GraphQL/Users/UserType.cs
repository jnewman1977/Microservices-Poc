using GraphQL.Types;
using Nitro.Msvc.User.Entities;

namespace Nitro.GraphQL.Tenants;

public class UserType : ObjectGraphType<User>
{
    public UserType()
    {
        Field(x => x.UserId, nullable: false, type: typeof(StringGraphType));
        Field(x => x.UserName, nullable: false, type: typeof(StringGraphType));
        Field(x => x.FirstName, nullable: false, type: typeof(StringGraphType));
        Field(x => x.LastName, nullable: false, type: typeof(StringGraphType));
    }
}
