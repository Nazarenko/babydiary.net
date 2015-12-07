using System.ComponentModel.DataAnnotations;

namespace BabyDiary.Models.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress(ErrorMessage = null)]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[!-~]*$")]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}