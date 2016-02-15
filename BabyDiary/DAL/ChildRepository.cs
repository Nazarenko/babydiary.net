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

        public async Task<Child> CreateChildAsync(Child child)
        {
            ctx.Child.Add(child);
            await ctx.SaveChangesAsync();
            return child;
        }

        public async Task<bool> DeleteChildAsync(Child child)
        {
            ctx.Entry(child).State = EntityState.Deleted;
            await ctx.SaveChangesAsync();
            return true;
        }

        public async Task<Child> FindChildByIdAsync(long childId)
        {
            var query = from c in ctx.Child where c.ChildId.Equals(childId) select c;
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Child>> FindChildsForUserAsync(long userId)
        {
            var query = from c in ctx.Child where c.UserId.Equals(userId) select c;
            return await query.ToListAsync();
        }
    }
}