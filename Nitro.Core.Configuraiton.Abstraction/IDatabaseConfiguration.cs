namespace Nitro.Core.Configuration.Abstraction;

public interface IDatabaseConfiguration
{
    public string ConnectionString { get; }

    public string DatabaseName { get; }
}