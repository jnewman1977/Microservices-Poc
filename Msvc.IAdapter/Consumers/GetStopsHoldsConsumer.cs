using MassTransit;

using Microsoft.Extensions.Logging;

using Msvc.IAdapter.Abstraction;
using Msvc.IAdapter.Factories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JackHenry.JHAContractTypes;

namespace Msvc.IAdapter.Consumers;

public class GetStopsHoldsConsumer : IConsumer<GetStopsHoldsRequest>
{
    public GetStopsHoldsConsumer(ILogger<GetStopsHoldsConsumer> logger, IIAdapterFactory iAdapterFactory)
    {
        this.Logger = logger;
        this.IAdapterFactory = iAdapterFactory;
    }

    public ILogger<GetStopsHoldsConsumer> Logger { get; }
    public IIAdapterFactory IAdapterFactory { get; }

    public async Task Consume(ConsumeContext<GetStopsHoldsRequest> context)
    {
        var response = new GetStopsHoldsResponse();

        try
        {
            var request = context.Message;

            JackHenry.Banking.iAdapter.MasterAdapterSetting masterAdapterSetting = new() 
            {
                EncryptedKey = request.MasterAdapterSettings.EncryptedKey,
                Port = request.MasterAdapterSettings.Port,
                Server = request.MasterAdapterSettings.Server,
                ServicePrincipalName = request.MasterAdapterSettings.ServicePrincipalName,
                IAdapterInstitutionId = request.MasterAdapterSettings.IAdapterInstitutionId,
            };

            var service = IAdapterFactory.Build(masterAdapterSetting);

            var result = await service.SearchAsync<StopChkSrchRs_MType>(request.RequestXml, request.InstitutionId);

            response.RecordCount = result.Payload_Rs.RecordsCount;

            response.Success = true;
        }
        catch (Exception e)
        {
            response.Success = false;
            this.Logger.LogError(e, "Error retrieving stops holds");
        }

        await context.RespondAsync(response);
    }
}
