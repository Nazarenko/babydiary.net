using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabyDiary.Models.DTOs
{
    public class DiaryDto
    {
        public DiaryDto(DateTime dateCreated, DateTime dateModified)
        {
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public long DiaryId { get; set; }

        public long ChildId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public DateTime DateCreated { get; }

        public DateTime DateModified { get; }
    }
}