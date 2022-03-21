using System;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Nitro.Core.Configuration;
using Nitro.Core.Configuration.Abstraction;
using Nitro.Msvc.Tenant.Access;
using Nitro.Msvc.Tenant.Configuration;

namespace Nitro.Msvc.Tenant;

public class Program
{
    private static bool? isRunningInContainer;

    private static bool IsRunningInContainer =>
        isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
            out var inContainer) && inContainer;

    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
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

                        cfg.Host(IsRunningInContainer ? "rabbitmq" : messagingConfig.Host,
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

                // Add all the Dependencies

                services
                    // Configuration
                    .AddTransient<IMessagingConfiguration, MessagingConfiguration>()
                    .AddTransient<IDatabaseConfiguration, DatabaseConfiguration>()
                    // Data Access
                    .AddTransient<IMongoClient>(p => new MongoClient(p.GetRequiredService<IDatabaseConfiguration>().ConnectionString))
                    .AddTransient<ITenantRepository, TenantRepository>()
                    // MassTransit
                    .AddMassTransitHostedService(true);
            });
    }
}