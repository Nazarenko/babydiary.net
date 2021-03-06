namespace BabyDiary.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User : BaseEntity
    {
        public long UserId { get; set; }

        [StringLength(32)]
        public string Sid { get; set; }

        [Required]
        [StringLength(32)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(176)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(32)]
        public string ActivatedToken { get; set; }

        public bool Activated { get; set; }

        [StringLength(32)]
        public string ResetPasswordToken { get; set; }

    }
}
