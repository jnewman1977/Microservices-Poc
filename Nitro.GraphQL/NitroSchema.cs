using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using Nitro.GraphQL.Interfaces;

namespace Nitro.GraphQL;

public class NitroSchema : Schema
{
    public NitroSchema(IRootQuery rootQuery, IServiceProvider services) : base(services)
    {
        Query = rootQuery;
    }
}
