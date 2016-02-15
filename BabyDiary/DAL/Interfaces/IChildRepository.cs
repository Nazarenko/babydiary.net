using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IChildRepository : IRepository<Child>
    {
        Task<Child> FindChildByIdAsync(long childId);
        Task<List<Child>> FindChildsForUserAsync(long userId);
        Task<Child> CreateChildAsync(Child child);
        Task<bool> DeleteChildAsync(Child child);
    }
}
