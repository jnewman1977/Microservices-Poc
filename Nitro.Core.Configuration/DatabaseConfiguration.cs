using Microsoft.Extensions.Configuration;
using Nitro.Core.Configuration.Abstraction;

namespace Nitro.Core.Configuration;

public class DatabaseConfiguration : IDatabaseConfiguration
{
    private readonly IConfiguration configuration;

    public DatabaseConfiguration(
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