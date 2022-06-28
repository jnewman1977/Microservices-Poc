using GraphQL;
using GraphQL.Types;
using Nitro.GraphQL.Interfaces;
using Nitro.Msvc.User.Entities;
using Nitro.Msvc.User.Messaging.Abstraction;
using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.GraphQL.Tenants;

public class UserQuery : ObjectGraphType, IUserQuery
{
    private readonly IUserServiceClient userServiceClient;

    public UserQuery(IUserServiceClient userServiceClient)
    {
        this.userServiceClient = userServiceClient;

        FieldAsync<ListGraphType<UserType>, IEnumerable<User>>("all",
            resolve: GetAllUsersAsync);

        FieldAsync<UserType, User>("byUserId",
            resolve: GetUserByUserIdAsync, arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "userId" }));
    }

    private async Task<IEnumerable<User>?> GetAllUsersAsync(IResolveFieldContext<object?> arg)
    {
        var result = await userServiceClient
            .GetAllUsersAsync(new GetAllUsersRequest(), CancellationToken.None)
            .ConfigureAwait(true);

        return result.Users;
    }

    private async Task<User?> GetUserByUserIdAsync(IResolveFieldContext<object?> arg)
    {
        var userId = arg.GetArgument<string>("userId");

        var result = await userServiceClient
            .GetUserByUserIdAsync(new GetUserByUserIdRequest
            {
                UserId = userId
            }, CancellationToken.None)
            .ConfigureAwait(true);

        return result.User;
    }
}