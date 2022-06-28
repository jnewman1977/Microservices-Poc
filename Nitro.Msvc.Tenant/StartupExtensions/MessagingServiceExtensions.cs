using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Nitro.Core.Configuration;
using Nitro.Core.Configuration.Abstraction;
using Nitro.Msvc.Tenant.Access;
using Nitro.Msvc.Tenant.Configuration;

namespace Nitro.Msvc.Tenant.StartupExtensions;

internal static class MessagingServiceExtensions
{
    public static IServiceCollection AddMessagingConfigurtion(this IServiceCollection services,
        bool isRunningInContainer)
    {
        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.SetKebabCaseEndpointNameFormatter();

            // By default, sagas are in-memory, but should be changed to a durable
            // saga repository.
            // x.SetInMemorySagaRepositoryProvider();

            var entryAssembly = Assembly.GetEntryAssembly();

            x.AddConsumers(entryAssembly);
            // x.AddSagaStateMachines(entryAssembly);
            // x.AddSagas(entryAssembly);
            // x.AddActivities(entryAssembly);

            x.UsingRabbitMq((context, cfg) =>
            {
                var messagingConfig = context.GetRequiredService<IMessagingConfiguration>();

                cfg.Host(isRunningInContainer ? "rabbitmq" : messagingConfig.Host,
                    messagingConfig.VirtualHost, hostConfig =>
                    {
                        hostConfig.Username(messagingConfig.UserName);
                        hostConfig.Password(messagingConfig.Password);
                    });

                cfg.AutoStart = true;
                cfg.AutoDelete = true;
                cfg.ConcurrentMessageLimit = messagingConfig.ConcurrentMessageLimit;

                cfg.UseDelayedMessageScheduler();
                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }

    public static IServiceCollection AddMessagingDependencies(this IServiceCollection services)
    {
        services
            // Configuration
            .AddTransient<IMessagingConfiguration, MessagingConfiguration>()
            .AddTransient<IDatabaseConfiguration, DatabaseConfiguration>()
            // Data Access
            .AddTransient<IMongoClient>(p =>
                new MongoClient(p.GetRequiredService<IDatabaseConfiguration>().ConnectionString))
            .AddTransient<ITenantRepository, TenantRepository>()
            // MassTransit
            .AddMassTransitHostedService(true);

        return services;
    }
}