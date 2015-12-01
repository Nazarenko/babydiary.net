using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyDiary.Models.Entities
{
    public class BaseEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ModifiedDate { get; set; }

        public bool Enabled { get; set; }
    }
}