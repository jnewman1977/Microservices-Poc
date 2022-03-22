using Microsoft.AspNetCore.Mvc;
using Nitro.Msvc.User.Messaging.Abstraction.Model;
using Nitro.Msvc.User.Messaging.Abstraction;

namespace Nitro.Channel.Api.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly ILogger<TenantController> logger;
    private readonly IUserServiceClient userServiceClient;

    public UserController(
        ILogger<TenantController> logger,
        IUserServiceClient userServiceClient)
    {
        this.logger = logger;
        this.userServiceClient = userServiceClient;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add([FromBody] AddUserRequest request)
    {
        try
        {
            var results = await userServiceClient
                .AddUserAsync(request, CancellationToken.None)
                .ConfigureAwait(true);

            return Ok(results);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error calling {nameof(Add)}");
            return Problem();
        }
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var results = await userServiceClient
                .GetAllUsersAsync(new GetAllUsersRequest(), CancellationToken.None)
                .ConfigureAwait(true);

            return Ok(results);
        }
        catch (Exception e)
        {
            logger.LogError(e, $"Error calling {nameof(GetAll)}");
            return Problem();
        }
    }
}