namespace BabyDiary.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Child")]
    public partial class Child : BaseEntity
    {
        public long ChildId { get; set; }

        public long UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(5)]
        public string BirthTime { get; set; }

        [StringLength(255)]
        public string BirthPlace { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }
    }
}
