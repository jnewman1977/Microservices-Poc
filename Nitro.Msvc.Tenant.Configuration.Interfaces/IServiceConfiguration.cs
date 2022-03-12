namespace Nitro.Msvc.Tenant.Configuration.Interfaces;

public interface IServiceConfiguration
{
    public string ConnectionString { get; }

    public string DatabaseName { get; }
}