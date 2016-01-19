using System.Threading.Tasks;
using BabyDiary.Models.DTOs;

namespace BabyDiary.Business.Interfaces
{
    public interface IUserProvider
    {
        Task<bool> IsEmailAvailableAsync(string email);
        Task<bool> IsLoginAvailableAsync(string login);
        Task CreateNewUserAsync(SignUpDto signUpDto);
        Task<bool> ActivateUserAsync(string token);
        Task<SignInInfoDto> SignInAsync(SignInDto signInDto);
        Task<string> GeneratePasswordResetTokenAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<bool> LoadUserBySidAsync(string sid);

        void ChangePassword(string passwordOld, string passwordNew);


    }
}
