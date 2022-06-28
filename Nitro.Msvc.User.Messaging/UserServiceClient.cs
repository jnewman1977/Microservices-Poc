using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.User.Messaging.Abstraction;
using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.Msvc.User.Messaging;

public class UserServiceClient : IUserServiceClient
{
    private readonly IBusControl busControl;
    private readonly ILogger<UserServiceClient> logger;

    public UserServiceClient(
        ILogger<UserServiceClient> logger,
        IBusControl busControl)
    {
        this.logger = logger;
        this.busControl = busControl;

        GetAllUsersClient = busControl.CreateRequestClient<GetAllUsersRequest>(TimeSpan.FromMinutes(1));

        AddUserClient = busControl.CreateRequestClient<AddUserRequest>(TimeSpan.FromMinutes(1));

        GetUserByUserIdClient = busControl.CreateRequestClient<GetUserByUserIdRequest>(TimeSpan.FromMinutes(1));
    }

    private IRequestClient<GetAllUsersRequest> GetAllUsersClient { get; }

    private IRequestClient<GetUserByUserIdRequest> GetUserByUserIdClient { get; }

    private IRequestClient<AddUserRequest> AddUserClient { get; }

    public async Task<GetAllUsersResponse> GetAllUsersAsync(
        GetAllUsersRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetAllUsersClient
                .GetResponse<GetAllUsersResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetAllUsersAsync));
            return new GetAllUsersResponse { Success = false, Errors = new[] { e.Message } };
        }
    }

    public async Task<GetUserByUserIdResponse> GetUserByUserIdAsync(
        GetUserByUserIdRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetUserByUserIdClient
                .GetResponse<GetUserByUserIdResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetUserByUserIdAsync));
            return new GetUserByUserIdResponse { Success = false, Errors = new[] { e.Message } };
        }
    }

    public async Task<AddUserResponse> AddUserAsync(
        AddUserRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await AddUserClient
                .GetResponse<AddUserResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetAllUsersAsync));
            return new AddUserResponse { Success = false, Errors = new[] { e.Message } };
        }
    }
}