namespace BabyDiary.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User : BaseEntity
    {
        public long UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(100)]
        public string ActivatedToken { get; set; }

        public bool Activated { get; set; }

        [StringLength(100)]
        public string ResetPasswordToken { get; set; }

    }
}
