using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.User.Access;
using Nitro.Msvc.User.Messaging.Abstraction.Model;

namespace Nitro.Msvc.User.Consumers;

public class AddUserConsumer : IConsumer<AddUserRequest>
{
    private readonly ILogger<AddUserConsumer> logger;
    private readonly IUserRepository userRepository;

    public AddUserConsumer(
        ILogger<AddUserConsumer> logger,
        IUserRepository UserRepository)
    {
        this.logger = logger;
        userRepository = UserRepository;
    }

    public async Task Consume(ConsumeContext<AddUserRequest> context)
    {
        var response = new AddUserResponse();

        var message = context.Message;

        try
        {
            await userRepository.InsertAsync(new Access.User
            {
                UserName = message.UserName,
                FirstName = message.FirstName,
                LastName = message.LastName
            }).ConfigureAwait(true);

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            logger.LogError(e, "Error adding User {UserName} | {FirstName} | {LastName}",
                message.UserName, message.FirstName, message.LastName);
        }

        await context.RespondAsync(response);
    }
}