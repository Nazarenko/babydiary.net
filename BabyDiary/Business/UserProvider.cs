using BabyDiary.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabyDiary.Business
{
    public class UserProvider : IUserProvider
    {
        public bool IsEmailAvailable(string Email)
        {
            return false;
        }
    }
}