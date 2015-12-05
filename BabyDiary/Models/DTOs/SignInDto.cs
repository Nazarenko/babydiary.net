using System.ComponentModel.DataAnnotations;

namespace BabyDiary.Models.DTOs
{
    public class SignInDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}