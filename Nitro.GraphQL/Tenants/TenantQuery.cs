using GraphQL;
using GraphQL.Types;
using Nitro.GraphQL.Interfaces;
using Nitro.Msvc.Tenant.Entities;
using Nitro.Msvc.Tenant.Messaging.Abstraction;
using Nitro.Msvc.Tenant.Messaging.Abstraction.Model;

namespace Nitro.GraphQL;

public class TenantQuery : ObjectGraphType, ITenantQuery
{
    private readonly ITenantServiceClient tenantServiceClient;

    public TenantQuery(ITenantServiceClient tenantServiceClient)
    {
        this.tenantServiceClient = tenantServiceClient;

        FieldAsync<ListGraphType<TenantType>, IEnumerable<Tenant>>("all",
            resolve: GetAllTenantsAsync);
    }

    private async Task<IEnumerable<Tenant>?> GetAllTenantsAsync(IResolveFieldContext<object?> arg)
    {
        var result = await tenantServiceClient
            .GetAllTenantsAsync(new GetAllTenantsRequest(), CancellationToken.None)
            .ConfigureAwait(true);

        return result.Tenants;
    }
}