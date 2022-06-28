namespace Nitro.Msvc.User.Access;

public interface IUserRepository
{
    Task InsertAsync(User user);

    Task UpdateAsync(User user);

    Task<IEnumerable<User>> GetAllAsync();

    Task<IEnumerable<User>> GetAllWithNameLikeAsync(string userNameLike);

    Task<User?> GetUserByIdAsync(string userId);

    Task<User?> GetUserByUserNameAsync(string userName);
}