﻿using GraphQL.DataLoader;
using GraphQL.Server;
using Nitro.GraphQL;
using GraphQL.SystemReactive;
using GraphQL;
using GraphQL.Execution;
using Nitro.GraphQL.Interfaces;
using Nitro.GraphQL.Tenants;
using GraphQLMicrosoftDI = GraphQL.MicrosoftDI.GraphQLBuilderExtensions;

namespace Nitro.Channel.Api.Extensions
{
    public static class GraphQLServiceExtensions
    {
        public static IServiceCollection AddGraphQLEnvironment(this IServiceCollection services,
            bool enableMetrics, bool exposeExceptions)
        {
            services
                .AddSingleton<IRootQuery, RootQuery>()
                .AddSingleton<ITenantQuery, TenantQuery>()
                .AddSingleton<IUserQuery, UserQuery>();

            GraphQLMicrosoftDI.AddGraphQL(services)
                .AddSubscriptionDocumentExecuter()
                .AddServer(true)
                .AddSchema<NitroSchema>()
                .ConfigureExecution(options =>
                {
                    options.EnableMetrics = enableMetrics;
                    var logger = options.RequestServices?.GetRequiredService<ILogger<Program>>();
                    options.UnhandledExceptionDelegate = context =>
                        logger?.LogError(message: "{OriginalException} ocurred", context.OriginalException.Message);
                })
                .Configure<ErrorInfoProviderOptions>(opt => opt.ExposeExceptionStackTrace = exposeExceptions)
                .AddSystemTextJson()
                .AddWebSockets()
                .AddDataLoader()
                .AddGraphTypes(typeof(NitroSchema).Assembly);

            return services;
        }

        public static WebApplication UseGraphQLEnvironment(this WebApplication app, string graphQlEndpoint = "/graphql")
        {
            app.UseGraphQLWebSockets<NitroSchema>();
            app.UseGraphQL<NitroSchema>(graphQlEndpoint);
            app.UseGraphQLAltair();
            app.UseGraphQLVoyager();

            return app;
        }
    }
}
