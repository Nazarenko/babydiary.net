using BabyDiary.DAL.Interfaces;
using System.Linq;
using BabyDiary.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BabyDiary.DAL.FilterSearch;

namespace BabyDiary.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BabyDiaryContext context) : base(context)
        {
        }

        public async Task<User> CreateUserAsync(User user)
        {
            ctx.Users.Add(user);
            await ctx.SaveChangesAsync();
            return user;
        }

        public async Task<User> FindUserByAsync(Filter filter)
        {
            var query = from u in ctx.Users select u;
            var deleg = ExpressionBuilder.GetExpression<User>(filter);
            return await  query.Where(deleg).FirstOrDefaultAsync();
        }

        public async Task<User> FindUserByAsync(IList<Filter> filters)
        {
            var query = from u in ctx.Users select u;
            var deleg = ExpressionBuilder.GetExpression<User>(filters);
            return await query.Where(deleg).FirstOrDefaultAsync();
        }

    }
}