using MassTransit;

using Microsoft.Extensions.Logging;

using Msvc.IAdapter.Abstraction;

namespace Msvc.IAdapter.Messaging;

public class IAdapterClient : IIAdapterClient
{
    private readonly IBusControl busControl;
    private readonly ILogger<IAdapterClient> logger;

    public IAdapterClient(
        ILogger<IAdapterClient> logger,
        IBusControl busControl)
    {
        this.logger = logger;
        this.busControl = busControl;

        GetStopsHoldsClient = busControl.CreateRequestClient<GetStopsHoldsRequest>(TimeSpan.FromMinutes(1));
    }

    private IRequestClient<GetStopsHoldsRequest> GetStopsHoldsClient { get; }

    public async Task<GetStopsHoldsResponse> GetStopsHoldsAsync(
        GetStopsHoldsRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetStopsHoldsClient
                .GetResponse<GetStopsHoldsResponse>(request, cancellationToken)
                .ConfigureAwait(true);

            return response.Message;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error calling {Method}", nameof(GetStopsHoldsAsync));
            return new GetStopsHoldsResponse { Success = false, Errors = new[] { e.Message } };
        }
    }

}
