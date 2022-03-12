using MassTransit;
using Nitro.Msvc.Tenant.Configuration;
using Nitro.Msvc.Tenant.Configuration.Interfaces;
using Nitro.Msvc.Tenant.Messaging;
using Nitro.Msvc.Tenant.Messaging.Interfaces;

bool? isRunningInContainer = null;

bool IsRunningInContainer = isRunningInContainer ??= 
    bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"),
        out var inContainer) && inContainer;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddLogging()
    .AddTransient<IConfiguration>(p => builder.Configuration)
    .AddTransient<IMessagingConfiguration, MessagingConfiguration>()
    .AddMassTransit(x =>
    {
        x.SetKebabCaseEndpointNameFormatter();
        x.UsingRabbitMq((context, config) =>
        {
            var messagingConfig = context.GetRequiredService<IMessagingConfiguration>();
            
            config.Host(IsRunningInContainer ? "rabbitmq" : messagingConfig.Host,
                messagingConfig.VirtualHost, configurator =>
                {
                    configurator.Username(messagingConfig.UserName);
                    configurator.Password(messagingConfig.Password);
                });
        });
    })
    .AddTransient<ITenantServiceClient, TenantServiceClient>()
    .AddMassTransitHostedService(true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();