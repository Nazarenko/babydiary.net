using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IUserRepository
    {
        User FindUserByEmail(string email);
        User FindUserByLogin(string login);
        User CreateUser(User user);
        User UpdateUser(User user);
    }
}
