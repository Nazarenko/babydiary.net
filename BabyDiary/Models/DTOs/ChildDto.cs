using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyDiary.Models.DTOs
{
    public class ChildDto
    {
        public long ChildId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string Surname { get; set; }

        public DateTime? BirthDate { get; set; }

        [RegularExpression(@"^([01][0-9]|2[0-3]):([0-5][0-9])$")]
        public string BirthTime { get; set; }

        [StringLength(255)]
        public string BirthPlace { get; set; }

        public byte Sex { get; set; }

        public List<DiaryDto> Diaries { get; set; }
    }
}