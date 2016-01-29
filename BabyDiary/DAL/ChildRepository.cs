using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.DAL.Interfaces;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL
{
    public class ChildRepository : Repository<Child>, IChildRepository
    {
        public ChildRepository(BabyDiaryContext context) : base(context)
        {
        }

        public Task<List<Child>> FindChildsByAsync(IList<Filter> filters)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Child>> FindChildsForUserAsync(long userId)
        {
            var query = from c in ctx.Child where c.UserId.Equals(userId) select c;
            return await query.ToListAsync();
        }
    }
}