using Microsoft.Extensions.Configuration;
using Nitro.Core.Configuration.Abstraction;

namespace Nitro.Msvc.Tenant.Configuration;

public class MessagingConfiguration : IMessagingConfiguration
{
    private readonly IConfiguration configuration;

    public MessagingConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration.GetSection("Settings:Messaging");
    }

    public int? ConcurrentMessageLimit => configuration.GetValue<int?>("ConcurrentMessageLimit", null);

    public string Host => configuration.GetValue<string>("Host");

    public string VirtualHost => configuration.GetValue<string>("VirtualHost");

    public string ReceiveEndpoint => configuration.GetValue<string>("ReceiveEndpoint");

    public string UserName => configuration.GetValue<string>("UserName");

    public string Password => configuration.GetValue<string>("Password");
}