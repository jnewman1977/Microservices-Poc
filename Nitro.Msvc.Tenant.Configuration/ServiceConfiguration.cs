using Microsoft.Extensions.Configuration;
using Nitro.Msvc.Tenant.Configuration.Interfaces;

namespace Nitro.Msvc.Tenant.Configuration;

public class ServiceConfiguration : IServiceConfiguration
{
    private readonly IConfiguration configuration;

    public ServiceConfiguration(
        IConfiguration configuration,
        IMessagingConfiguration messagingConfiguration)
    {
        this.configuration = configuration;
        MessagingConfiguration = messagingConfiguration;
    }

    private IMessagingConfiguration MessagingConfiguration { get; }

    public string ConnectionString => configuration.GetConnectionString(configuration["Settings:Database:ConnectionString"]);

    public string DatabaseName => configuration["Settings:Database:DatabaseName"];
}