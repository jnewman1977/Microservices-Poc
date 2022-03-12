using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.Tenant.Access;
using Nitro.Msvc.Tenant.Messaging.Interfaces.Model;

namespace Nitro.Msvc.Tenant.Consumers;

public class GetAllTenantsConsumer : IConsumer<GetAllTenantsRequest>
{
    private readonly ILogger<GetAllTenantsConsumer> logger;
    private readonly ITenantRepository tenantRepository;

    public GetAllTenantsConsumer(
        ILogger<GetAllTenantsConsumer> logger,
        ITenantRepository tenantRepository)
    {
        this.logger = logger;
        this.tenantRepository = tenantRepository;
    }

    public async Task Consume(ConsumeContext<GetAllTenantsRequest> context)
    {
        var response = new GetAllTenantsResponse();

        try
        {
            var results = await tenantRepository.GetAllAsync().ConfigureAwait(true);

            response.Tenants = results
                .Select(tenant => new Entities.Tenant
                {
                    TenantId = tenant.TenantId,
                    Name = tenant.Name
                })
                .ToArray();

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            logger.LogError(e, "Error retrieving all tenants");
        }

        await context.RespondAsync(response);
    }
}