using Nitro.Msvc.Tenant.Messaging.Interfaces.Model;

namespace Nitro.Msvc.Tenant.Messaging.Interfaces;

public interface ITenantServiceClient
{
    Task<GetAllTenantsResponse> GetAllTenantsAsync(
        GetAllTenantsRequest request,
        CancellationToken cancellationToken);

    Task<AddTenantResponse> AddTenantAsync(
        AddTenantRequest request,
        CancellationToken cancellationToken);
}