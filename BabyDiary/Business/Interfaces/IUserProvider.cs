using BabyDiary.Models.DTOs;

namespace BabyDiary.Business.Interfaces
{
    public interface IUserProvider
    {
        bool IsEmailAvailable(string email);
        bool IsLoginAvailable(string login);
        void CreateNewUser(SignUpDto signUpDto);
        void ChangePassword(string passwordOld, string passwordNew);
        void ActivateUser(string hash);
    }
}
