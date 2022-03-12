using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nitro.Msvc.Tenant.Access;
using Nitro.Msvc.Tenant.Messaging.Interfaces.Model;

namespace Nitro.Msvc.Tenant.Consumers
{
    public class AddTenantConsumer : IConsumer<AddTenantRequest>
    {
        private readonly ILogger<AddTenantConsumer> logger;
        private readonly ITenantRepository tenantRepository;

        public AddTenantConsumer(
            ILogger<AddTenantConsumer> logger,
            ITenantRepository tenantRepository)
        {
            this.logger = logger;
            this.tenantRepository = tenantRepository;
        }

        public async Task Consume(ConsumeContext<AddTenantRequest> context)
        {
            var response = new AddTenantResponse();

            var message = context.Message;

            try
            {
                await tenantRepository.InsertAsync(new Access.Tenant
                {
                    Name = message.Name
                }).ConfigureAwait(true);

                response.Success = true;
            }
            catch (Exception e)
            {
                response.Success = false;
                logger.LogError(e, $"Error adding tenant {message.Name}");
            }

            await context.RespondAsync(response);
        }
    }
}
