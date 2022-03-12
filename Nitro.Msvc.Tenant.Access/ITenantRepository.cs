namespace Nitro.Msvc.Tenant.Access;

public interface ITenantRepository
{
    Task InsertAsync(Tenant tenant);

    Task UpdateAsync(Tenant tenant);

    Task<IEnumerable<Tenant>> GetAllAsync();

    Task<IEnumerable<Tenant>> GetAllWithNameLikeAsync(string nameLike);

    Task<Tenant> GetTenantByIdAsync(string tenantId);

    Task<Tenant> GetTenantByNameAsync(string name);
}