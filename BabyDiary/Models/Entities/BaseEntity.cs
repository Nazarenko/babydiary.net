using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyDiary.Models.Entities
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool Enabled { get; set; }
    }
}