using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.User.Access;
using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.Msvc.User.Consumers;

public class GetUserByUserIdConsumer : IConsumer<GetUserByUserIdRequest>
{
    private readonly ILogger<GetUserByUserIdConsumer> logger;
    private readonly IUserRepository userRepository;

    public GetUserByUserIdConsumer(
        ILogger<GetUserByUserIdConsumer> logger,
        IUserRepository userRepository)
    {
        this.logger = logger;
        this.userRepository = userRepository;
    }

    public async Task Consume(ConsumeContext<GetUserByUserIdRequest> context)
    {
        var request = context.Message;
        var response = new GetUserByUserIdResponse();

        try
        {
            var result = await userRepository
                .GetUserByIdAsync(request.UserId)
                .ConfigureAwait(true);

            response.User = result switch
            {
                null => null,
                _ => new Nitro.Msvc.User.Entities.User
                {
                    UserId = result.UserId,
                    UserName = result.UserName,
                    FirstName = result.FirstName,
                    LastName = result.LastName
                }
            };

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            logger.LogError(e, "Error retrieving user by id {userId}", request.UserId);
        }

        await context.RespondAsync(response);
    }
}