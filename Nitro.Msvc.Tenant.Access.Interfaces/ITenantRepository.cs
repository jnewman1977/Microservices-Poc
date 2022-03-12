namespace Nitro.Msvc.Tenant.Access.Interfaces;

public interface ITenantRepository
{
    IEnumerable<Tenant> GetAll();

    IEnumerable<Tenant> GetAllWithNameLike(string nameLike);

    Tenant GetTenantById(string tenantId);

    Tenant GetTenantByName(string name);
}