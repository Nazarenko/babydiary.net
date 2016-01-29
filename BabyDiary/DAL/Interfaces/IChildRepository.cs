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
        Task<List<Child>> FindChildsByAsync(IList<Filter> filters);
        Task<List<Child>> FindChildsForUserAsync(long userId);
    }
}
