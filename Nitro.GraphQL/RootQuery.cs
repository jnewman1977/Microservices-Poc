using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Nitro.GraphQL;

public class RootQuery : ObjectGraphType, IRootQuery
{
    public RootQuery(IServiceProvider services)
    {
        Field<ITenantQuery>("tenants", resolve: context => 
            services.GetRequiredService<ITenantQuery>());
    }
}
