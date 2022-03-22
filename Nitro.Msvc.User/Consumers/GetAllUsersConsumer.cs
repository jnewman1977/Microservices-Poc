using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.User.Access;
using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.Msvc.User.Consumers;

public class GetAllUsersConsumer : IConsumer<GetAllUsersRequest>
{
    private readonly ILogger<GetAllUsersConsumer> logger;
    private readonly IUserRepository userRepository;

    public GetAllUsersConsumer(
        ILogger<GetAllUsersConsumer> logger,
        IUserRepository userRepository)
    {
        this.logger = logger;
        this.userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<GetAllUsersRequest> context)
    {
        var response = new GetAllUsersResponse();

        try
        {
            var results = await userRepository.GetAllAsync().ConfigureAwait(true);

            response.Users = results
                .Select(user => new Entities.User
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                })
                .ToArray();

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            logger.LogError(e, "Error retrieving all Users");
        }

        await context.RespondAsync(response);
    }
}