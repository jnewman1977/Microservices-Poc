using GraphQL;
using GraphQL.Types;

using Msvc.IAdapter.Abstraction;

namespace Nitro.GraphQL;

public interface IStopsHoldsQuery : IObjectGraphType
{
    Task<GetStopsHoldsResponse?> GetStopsHoldsAsync(IResolveFieldContext<object?> arg);
}
