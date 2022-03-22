using Nitro.Msvc.Tenant.Messaging.Abstraction.Model;

namespace Nitro.Msvc.Tenant.Messaging.Abstraction;

public interface ITenantServiceClient
{
    Task<GetAllTenantsResponse> GetAllTenantsAsync(
        GetAllTenantsRequest request,
        CancellationToken cancellationToken);

    Task<AddTenantResponse> AddTenantAsync(
        AddTenantRequest request,
        CancellationToken cancellationToken);
}