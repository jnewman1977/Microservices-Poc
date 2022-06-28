using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.Msvc.User.Messaging.Abstraction;

public interface IUserServiceClient
{
    Task<GetAllUsersResponse> GetAllUsersAsync(
        GetAllUsersRequest request,
        CancellationToken cancellationToken);

    Task<GetUserByUserIdResponse> GetUserByUserIdAsync(
        GetUserByUserIdRequest request,
        CancellationToken cancellationToken);

    Task<AddUserResponse> AddUserAsync(
        AddUserRequest request,
        CancellationToken cancellationToken);
}