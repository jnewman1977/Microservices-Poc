using MassTransit;
using Nitro.Msvc.Tenant.Configuration;
using Nitro.Msvc.Tenant.Configuration.Interfaces;
using Nitro.Msvc.Tenant.Messaging;
using Nitro.Msvc.Tenant.Messaging.Interfaces;
using GraphQL.DataLoader;
using GraphQL.Server;
using Nitro.GraphQL;
using GraphQL.SystemReactive;
using GraphQL;
using GraphQL.Execution;

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

builder.Services
    .AddSingleton<IRootQuery, RootQuery>()
    .AddSingleton<ITenantQuery, TenantQuery>();

GraphQL.MicrosoftDI.GraphQLBuilderExtensions.AddGraphQL(builder.Services)
    .AddSubscriptionDocumentExecuter()
    .AddServer(true)
    .AddSchema<NitroSchema>()
    .ConfigureExecution(options =>
    {
        options.EnableMetrics = builder.Environment.IsDevelopment();
        var logger = options.RequestServices.GetRequiredService<ILogger<Program>>();
        options.UnhandledExceptionDelegate = context =>
            logger.LogError(message: $"{context.OriginalException.Message} ocurred");
    })
    .Configure<ErrorInfoProviderOptions>(opt =>
        opt.ExposeExceptionStackTrace = builder.Environment.IsDevelopment())
    .AddSystemTextJson()
    .AddWebSockets()
    .AddDataLoader()
    .AddGraphTypes(typeof(NitroSchema).Assembly);

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

app.UseWebSockets();
app.UseGraphQLWebSockets<NitroSchema>();
app.UseGraphQL<NitroSchema>("/graphql");
app.UseGraphQLAltair();
app.UseGraphQLVoyager();

app.Run();