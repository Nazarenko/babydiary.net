using BabyDiary.Business.Interfaces;
using BabyDiary.DAL.Interfaces;
using System;
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

        public void ChangePassword(string passwordOld, string passwordNew)
        {
            throw new NotImplementedException();
        }

        public bool IsEmailAvailable(string email)
        {
            return _userRepository.FindUserByEmail(email) == null;
        }

        public bool IsLoginAvailable(string login)
        {
            return _userRepository.FindUserByLogin(login) == null;
        }

        public void SignUp(SignUpDto signUpDto)
        {
            User user = new User();
            user.Email = signUpDto.Email;
            user.Login = signUpDto.Login;
            user.Password = signUpDto.Password;
            _userRepository.CreateUser(user);
        }
    }
}