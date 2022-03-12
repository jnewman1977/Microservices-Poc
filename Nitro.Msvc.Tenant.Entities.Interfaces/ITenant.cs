namespace Nitro.Msvc.Tenant.Entities.Interfaces;

public interface ITenant
{
    public string TenantId { get; }

    public string Name { get; }
}