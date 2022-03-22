using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.Tenant.Messaging.Abstraction;
using Nitro.Msvc.Tenant.Messaging.Abstraction.Model;

namespace Nitro.Msvc.Tenant.Messaging;

public class TenantServiceClient : ITenantServiceClient
{
    private readonly IBusControl busControl;
    private readonly ILogger<TenantServiceClient> logger;

    public TenantServiceClient(
        ILogger<TenantServiceClient> logger,
        IBusControl busControl)
    {
        this.logger = logger;
        this.busControl = busControl;

        GetAllTenantsClient = busControl.CreateRequestClient<GetAllTenantsRequest>(TimeSpan.FromMinutes(1));

        AddTenantClient = busControl.CreateRequestClient<AddTenantRequest>(TimeSpan.FromMinutes(1));
    }

    private IRequestClient<GetAllTenantsRequest> GetAllTenantsClient { get; }

    private IRequestClient<AddTenantRequest> AddTenantClient { get; }

    public async Task<GetAllTenantsResponse> GetAllTenantsAsync(
        GetAllTenantsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetAllTenantsClient
                .GetResponse<GetAllTenantsResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetAllTenantsAsync));
            return new GetAllTenantsResponse { Success = false, Errors = new[] { e.Message } };
        }
    }

    public async Task<AddTenantResponse> AddTenantAsync(
        AddTenantRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await AddTenantClient
                .GetResponse<AddTenantResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetAllTenantsAsync));
            return new AddTenantResponse { Success = false, Errors = new[] { e.Message } };
        }
    }
}