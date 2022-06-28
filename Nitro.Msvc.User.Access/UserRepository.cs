using MongoDB.Driver;
using Nitro.Core.Configuration.Abstraction;

namespace Nitro.Msvc.User.Access;

public class UserRepository : IUserRepository
{
    private readonly IMongoClient mongoClient;
    private readonly IDatabaseConfiguration serviceConfiguration;

    public UserRepository(IDatabaseConfiguration databaseConfiguration,
        IMongoClient mongoClient)
    {
        serviceConfiguration = databaseConfiguration;
        this.mongoClient = mongoClient;
    }

    public async Task InsertAsync(User tenant)
    {
        var collection = GetCollection();
        await collection.InsertOneAsync(tenant);
    }

    public async Task UpdateAsync(User tenant)
    {
        var collection = GetCollection();
        var filter = Builders<User>.Filter.Eq(t => t.UserId, tenant.UserId);
        await collection.ReplaceOneAsync(filter, tenant);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var collection = GetCollection();
        return await collection.AsQueryable().ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllWithNameLikeAsync(string userNameLike)
    {
        var collection = GetCollection();
        var filter = Builders<User>.Filter.Gte(tenant => tenant.UserName, userNameLike);
        return await collection.OfType<User>().Find(filter).ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(string userId)
    {
        var collection = GetCollection();
        var filter = Builders<User>.Filter.Eq(tenant => tenant.UserId, userId);
        // return await collection.OfType<User>().Find(filter).FirstOrDefaultAsync();
        return await collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        var collection = GetCollection();
        var filter = Builders<User>
            .Filter.Eq(tenant => tenant.UserName, userName);

        return await collection.OfType<User>().Find(filter).FirstOrDefaultAsync();
    }

    private IMongoDatabase GetDatabase()
    {
        return mongoClient.GetDatabase(serviceConfiguration.DatabaseName);
    }

    private IMongoCollection<User> GetCollection()
    {
        var database = GetDatabase();
        return database.GetCollection<User>("User");
    }
}