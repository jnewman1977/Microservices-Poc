using Microsoft.Extensions.Hosting;
using Nitro.Msvc.User.StartupExtensions;

namespace Nitro.Msvc.User;

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
                services
                    .AddMessagingConfigurtion(IsRunningInContainer)
                    .AddMessagingDependencies();
            });
    }
}