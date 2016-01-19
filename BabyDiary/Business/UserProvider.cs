using BabyDiary.Business.Interfaces;
using BabyDiary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Helpers;
using BabyDiary.Models.DTOs;
using BabyDiary.Models.Entities;

namespace BabyDiary.Business
{
    public class UserProvider : IUserProvider
    {
        public User User
        {
            get { return _user; }
        }

        private readonly IUserRepository _userRepository;
        private User _user;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ActivateUserAsync(string token)
        {
            var user = await _userRepository.FindUserByAsync(new Filter("ActivatedToken", token));

            if (user == null) return false;

            user.Activated = true;
            user.ActivatedToken = null;
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<SignInInfoDto> SignInAsync(SignInDto signInDto)
        {
            var user = await _userRepository.FindUserByAsync(new Filter("Login", signInDto.Login));

            if (user == null || !PasswordHash.ValidatePassword(signInDto.Password,user.Password))
            {
                return new SignInInfoDto() {State = UserState.NotFound};
            }
            else
            {
                var result = new SignInInfoDto() {Sid = user.Sid, Name = user.Name, Login = user.Login, State = UserState.Success};
                if (!user.Activated) result.State = UserState.NotActivated;
                if (!user.Enabled) result.State = UserState.Locked;
                return result;
            }

        }

        public async Task<string> GeneratePasswordResetTokenAsync(string email)
        {
            var user = await _userRepository.FindUserByAsync(new List<Filter>()
            {
                new Filter("Email", email),
                new Filter("Activated", true),
                new Filter("Enabled", true)
            });

            if (user == null) return null;

            user.ResetPasswordToken = PasswordHash.GenerateToken();
            await _userRepository.SaveChangesAsync();
            return user.ResetPasswordToken;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _userRepository.FindUserByAsync(new List<Filter>()
            {
                new Filter("Email", model.Email),
                new Filter("ResetPasswordToken", model.Code),
                new Filter("Enabled", true)
            });

            if (user == null) return false;

            user.Password = PasswordHash.CreatePasswordHash(model.Password);
            user.ResetPasswordToken = null;
            user.Sid = PasswordHash.GenerateToken();
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public void ChangePassword(string passwordOld, string passwordNew)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            return await _userRepository.FindUserByAsync(new Filter("Email", email)) == null;
        }

        public async Task<bool> IsLoginAvailableAsync(string login)
        {
            return await _userRepository.FindUserByAsync(new Filter("Login", login)) == null;
        }

        public async Task CreateNewUserAsync(SignUpDto signUpDto)
        {
            User user = new User
            {
                Email = signUpDto.Email,
                Login = signUpDto.Login,
                Password = PasswordHash.CreatePasswordHash(signUpDto.Password),
                ActivatedToken = PasswordHash.GenerateToken(),
                Sid = PasswordHash.GenerateToken()
        };
            await _userRepository.CreateUserAsync(user);
        }

        public async Task<bool> LoadUserBySidAsync(string sid)
        {
            var user = await _userRepository.FindUserByAsync(new Filter("Sid", sid));
            _user = user;
            return user  != null;
        }
    }
}