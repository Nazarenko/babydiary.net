using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BabyDiary.Models
{
    public class SignUpDTO
    {
        [Required(ErrorMessage = "Пожалуйста, укажите e-mail")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Пожалуйста, укажите e-mail верного формата")]
        [System.Web.Mvc.Remote("IsEmailAvailble", "SignUp", ErrorMessage = "Этот e-mail уже зарегистрирован")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пожалуйста, укажите логин")]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$", ErrorMessage = "Логин может состоять только из латинских символов, цифр, одинарного дефиса или знака подчеркивания")]
        [StringLength(30, ErrorMessage = "Логин должен содержать от {2} до {1} символов", MinimumLength = 3)]
        [System.Web.Mvc.Remote("IsLoginAvailble", "SignUp", ErrorMessage = "Этот логин уже используется")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Необходимо ввести пароль")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^[!-~]*$", ErrorMessage = "Пароль содержит запрещённые символы")]
        [StringLength(255, ErrorMessage = "Пароль должен содержать от {2} до {1} символов", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль еще раз")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Подтверждение не совпадает с паролем")]
        public string ConfirmPassword { get; set; }
    }
}