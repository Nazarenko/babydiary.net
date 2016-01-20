using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BabyDiary.Business.Interfaces;
using BabyDiary.Models.Entities;

namespace BabyDiary.Business
{
    public class CurrentUser : ICurrentUser
    {
        public User User { get; set; }
        public bool IsEnabled()
        {
            return User != null && User.Enabled;
        }

        public string Login => User.Login;
    }
}