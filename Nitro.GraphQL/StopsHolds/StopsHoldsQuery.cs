using GraphQL;
using GraphQL.Types;

using Msvc.IAdapter.Abstraction;

using Nitro.GraphQL.Interfaces;

namespace Nitro.GraphQL;

public class StopsHoldsQuery : ObjectGraphType, IStopsHoldsQuery
{
    private readonly IIAdapterClient iAdapterClient;

    public StopsHoldsQuery(IIAdapterClient iAdapterClient)
    {
        this.iAdapterClient = iAdapterClient;

        FieldAsync<StopsHoldsType, GetStopsHoldsResponse>("all",
            arguments: new QueryArguments(
                new QueryArgument<StringGraphType> { Name = "requestXml" },
                new QueryArgument<MasterAdapterSettingsType> { Name = "settings" },
                new QueryArgument<StringGraphType> { Name = "institutionId" }),
            resolve: GetStopsHoldsAsync);
    }

    public async Task<GetStopsHoldsResponse?> GetStopsHoldsAsync(IResolveFieldContext<object?> arg)
    {
        var requestXml = arg.GetArgument<string>("requestXml");
        var masterAdapterSettings = arg.GetArgument<MasterAdapterSettings>("settings");
        var institutionId = arg.GetArgument<string>("institutionId");

        var result = await this.iAdapterClient
            .GetStopsHoldsAsync(new GetStopsHoldsRequest { 
                RequestXml = requestXml, 
                MasterAdapterSettings = masterAdapterSettings, 
                InstitutionId = institutionId }, CancellationToken.None)
            .ConfigureAwait(true);

        return result;
    }
}
