using System.Collections.Generic;
using System.Threading.Tasks;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> FindUserByAsync(IList<Filter> filters);
        Task<User> FindUserByAsync(Filter filter);
        Task<User> CreateUserAsync(User user);
    }
}
