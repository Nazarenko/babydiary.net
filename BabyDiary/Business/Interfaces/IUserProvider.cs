using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyDiary.Business.Interfaces
{
    public interface IUserProvider
    {
        bool IsEmailAvailable(string Email);
    }
}
