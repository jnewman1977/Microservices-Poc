using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Nitro.GraphQL.Interfaces;

namespace Nitro.GraphQL;

public class RootQuery : ObjectGraphType, IRootQuery
{
    public RootQuery(IServiceProvider services)
    {
        Field<ITenantQuery>("tenants", resolve: context => 
            services.GetRequiredService<ITenantQuery>());

        Field<IUserQuery>("users", resolve: context =>
            services.GetRequiredService<IUserQuery>());
    }
}
