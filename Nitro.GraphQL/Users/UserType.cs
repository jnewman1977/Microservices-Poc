using GraphQL.Types;
using Nitro.Msvc.User.Entities;

namespace Nitro.GraphQL.Tenants;

public class UserType : ObjectGraphType<User>
{
    public UserType()
    {
        Field(x => x.UserId, false, typeof(StringGraphType));
        Field(x => x.UserName, false, typeof(StringGraphType));
        Field(x => x.FirstName, false, typeof(StringGraphType));
        Field(x => x.LastName, false, typeof(StringGraphType));
    }
}