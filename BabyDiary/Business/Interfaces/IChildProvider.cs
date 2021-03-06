﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyDiary.Models.DTOs;

namespace BabyDiary.Business.Interfaces
{
    public interface IChildProvider
    {
        Task<List<ChildDto>> GetChildsAsync();
        Task<ChildDto> SaveChildAsync(ChildDto child);
        Task DeleteChildAsync(long childId);
    }
}
