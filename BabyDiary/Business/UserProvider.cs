using BabyDiary.Business.Interfaces;
using BabyDiary.DAL.Interfaces;
using System;
using BabyDiary.Business.Helpers;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.DTOs;
using BabyDiary.Models.Entities;

namespace BabyDiary.Business
{
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;

        public UserProvider(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ActivateUser(string hash)
        {
            User user = _userRepository.FindUserBy(new Filter("ActivatedHash", hash));
            if (user != null)
            {
                user.Activated = true;
                user.ActivatedHash = null;
                _userRepository.SaveChanges();
            }
            else
            {
                // TODO exception
            }
        }

        public SignInInfoDto GetUserSignIn(SignInDto signInDto)
        {
            var user = _userRepository.FindUserBy(new Filter("Login", signInDto.Login));

            if (user == null || !PasswordHash.ValidatePassword(signInDto.Password,user.Password))
            {
                return new SignInInfoDto() {State = UserState.NotFound};
            }
            else
            {
                var result = new SignInInfoDto() {Name = user.Name, Login = user.Login, State = UserState.Success};
                if (!user.Activated) result.State = UserState.NotActivated;
                if (!user.Enabled) result.State = UserState.Locked;
                return result;
            }

        }

        public void ChangePassword(string passwordOld, string passwordNew)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailAvailable(string email)
        {
            return _userRepository.FindUserBy(new Filter("Email", email)) == null;
        }

        public bool IsLoginAvailable(string login)
        {
            return _userRepository.FindUserBy(new Filter("Login", login)) == null;
        }

        public void CreateNewUser(SignUpDto signUpDto)
        {
            User user = new User
            {
                Email = signUpDto.Email,
                Login = signUpDto.Login,
                Password = PasswordHash.CreateHash(signUpDto.Password),
                ActivatedHash = PasswordHash.CreateRandomHash()
            };
            _userRepository.CreateUser(user);
        }
    }
}