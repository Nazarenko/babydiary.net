using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyDiary.Models.Entities;

namespace BabyDiary.Business.Interfaces
{
    public interface ICurrentUser
    {
        User User { get; set; }
        bool IsEnabled();
        String Login { get; }
    }
}
