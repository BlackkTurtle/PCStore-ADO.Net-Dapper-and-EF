using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.DAL.Entities;

namespace Forum.DAL.Repositories.Contracts
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<Users> GetUserByEmailAsync(string Email);
        Task<Users> GetUserByPhoneAsync(string Email);
    }
}
