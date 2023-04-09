using PCStoreEF.Entities;

namespace PCStoreEF.EFRepositories.Contracts;

public interface IEFUsersRepository : IEFGenericRepository<User>
{
    Task<User> GetUserByEmailAsync(string Email);
    Task<User> GetUserByPhoneAsync(string Email);
}