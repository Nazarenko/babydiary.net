using System.Collections.Generic;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserBy(IList<Filter> filters);
        User FindUserBy(Filter filter);
        User CreateUser(User user);
    }
}
