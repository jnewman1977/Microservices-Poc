using GraphQL;
using GraphQL.Types;
using Nitro.GraphQL.Interfaces;
using Nitro.Msvc.User.Messaging.Abstraction.Model;
using Nitro.Msvc.User.Messaging.Abstraction;
using Nitro.Msvc.User.Entities;

namespace Nitro.GraphQL.Tenants;

public class UserQuery : ObjectGraphType, IUserQuery
{
    private readonly IUserServiceClient userServiceClient;

    public UserQuery(IUserServiceClient userServiceClient)
    {
        this.userServiceClient = userServiceClient;

        FieldAsync<ListGraphType<UserType>, IEnumerable<User>>("all",
            resolve: GetAllUsersAsync);
    }

    private async Task<IEnumerable<User>?> GetAllUsersAsync(IResolveFieldContext<object?> arg)
    {
        var result = await userServiceClient
            .GetAllUsersAsync(new GetAllUsersRequest(), CancellationToken.None)
            .ConfigureAwait(true);

        return result.Users;
    }
}
