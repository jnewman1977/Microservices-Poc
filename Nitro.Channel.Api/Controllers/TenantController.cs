using Microsoft.AspNetCore.Mvc;
using Nitro.Msvc.Tenant.Entities;
using Nitro.Msvc.Tenant.Messaging.Interfaces;
using Nitro.Msvc.Tenant.Messaging.Interfaces.Model;

namespace Nitro.Channel.Api.Controllers;

[ApiController]
[Route("tenant")]
public class TenantController : ControllerBase
{
    private readonly ILogger<TenantController> logger;
    private readonly ITenantServiceClient tenantClient;

    public TenantController(
        ILogger<TenantController> logger,
        ITenantServiceClient tenantClient)
    {
        this.logger = logger;
        this.tenantClient = tenantClient;
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> Add([FromBody] AddTenantRequest request)
    {
        try
        {
            var results = await tenantClient
                .AddTenantAsync(request, CancellationToken.None)
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
            var results = await tenantClient
                .GetAllTenantsAsync(new GetAllTenantsRequest(), CancellationToken.None)
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