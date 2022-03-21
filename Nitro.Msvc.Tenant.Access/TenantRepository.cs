using MongoDB.Driver;
using Nitro.Core.Configuration.Abstraction;

namespace Nitro.Msvc.Tenant.Access;

public class TenantRepository : ITenantRepository
{
    private readonly IDatabaseConfiguration serviceConfiguration;
    private readonly IMongoClient mongoClient;

    public TenantRepository(IDatabaseConfiguration databaseConfiguration,
        IMongoClient mongoClient)
    {
        this.serviceConfiguration = databaseConfiguration;
        this.mongoClient = mongoClient;
    }

    public async Task InsertAsync(Tenant tenant)
    {
        var collection = GetCollection();
        await collection.InsertOneAsync(tenant);
    }

    public async Task UpdateAsync(Tenant tenant)
    {
        var collection = GetCollection();
        var filter = Builders<Tenant>.Filter.Eq(t => t.TenantId, tenant.TenantId);
        await collection.ReplaceOneAsync(filter, tenant);
    }

    public async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        var collection = GetCollection();
        return await collection.AsQueryable().ToListAsync();
    }

    public async Task<IEnumerable<Tenant>> GetAllWithNameLikeAsync(string nameLike)
    {
        var collection = GetCollection();
        var filter = Builders<Tenant>.Filter.Gte(tenant => tenant.Name, nameLike);
        return await collection.OfType<Tenant>().Find(filter).ToListAsync();
    }

    public async Task<Tenant> GetTenantByIdAsync(string tenantId)
    {
        var collection = GetCollection();
        var filter = Builders<Tenant>.Filter.Eq(tenant => tenant.TenantId, tenantId);
        return await collection.OfType<Tenant>().Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Tenant> GetTenantByNameAsync(string name)
    {
        var collection = GetCollection();
        var filter = Builders<Tenant>.Filter.Eq(tenant => tenant.Name, name);
        return await collection.OfType<Tenant>().Find(filter).FirstOrDefaultAsync();
    }

    private IMongoDatabase GetDatabase()
    {
        return mongoClient.GetDatabase(serviceConfiguration.DatabaseName);
    }

    private IMongoCollection<Tenant> GetCollection()
    {
        var database = GetDatabase();
        return database.GetCollection<Tenant>("Tenant");
    }
}