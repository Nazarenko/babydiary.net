using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyDiary.Models.DTOs
{
    public enum UserState
    {
        Success,
        NotActivated,
        Locked,
        NotFound
    }

    public class SignInInfoDto
    {
        public string Sid { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public UserState State { get; set; }
    }
}