using System.ComponentModel.DataAnnotations;

namespace BabyDiary.Models.DTOs
{
    public class SignUpDto
    {
        [Required]
        [EmailAddress(ErrorMessage = null)]
        [StringLength(100)]
        [System.Web.Mvc.Remote("IsEmailAvailble", "SignUp", HttpMethod = "Post", AdditionalFields = "__RequestVerificationToken")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_-]+$")]
        [StringLength(50, MinimumLength = 3)]
        [System.Web.Mvc.Remote("IsLoginAvailble", "SignUp", HttpMethod = "Post", AdditionalFields = "__RequestVerificationToken")]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"^[!-~]*$")]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}