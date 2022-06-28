using MassTransit;
using Nitro.Core.Configuration.Abstraction;
using Nitro.Msvc.Tenant.Configuration;
using Nitro.Msvc.Tenant.Messaging;
using Nitro.Msvc.Tenant.Messaging.Abstraction;
using Nitro.Msvc.User.Messaging;
using Nitro.Msvc.User.Messaging.Abstraction;

namespace Nitro.Channel.Api.StartupExtensions;

public static class MessagingServiceExtensions
{
    public static IServiceCollection AddMicroservices(this IServiceCollection services,
        bool isRunningInContainer)
    {
        services
            .AddTransient<IMessagingConfiguration, MessagingConfiguration>()
            .AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    var messagingConfig = context.GetRequiredService<IMessagingConfiguration>();

                    config.Host(isRunningInContainer ? "rabbitmq" : messagingConfig.Host,
                        messagingConfig.VirtualHost, configurator =>
                        {
                            configurator.Username(messagingConfig.UserName);
                            configurator.Password(messagingConfig.Password);
                        });
                });
            })
            .AddTransient<ITenantServiceClient, TenantServiceClient>()
            .AddTransient<IUserServiceClient, UserServiceClient>()
            .AddMassTransitHostedService(true);

        return services;
    }
}