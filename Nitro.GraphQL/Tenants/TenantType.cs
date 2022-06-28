using GraphQL.Types;
using Nitro.Msvc.Tenant.Entities;

namespace Nitro.GraphQL;

public class TenantType : ObjectGraphType<Tenant>
{
    public TenantType()
    {
        Field(x => x.TenantId, false, typeof(StringGraphType));
        Field(x => x.Name, false, typeof(StringGraphType));
    }
}