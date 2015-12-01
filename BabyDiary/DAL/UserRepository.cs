using BabyDiary.DAL.Interfaces;
using System.Linq;
using BabyDiary.Models.Entities;
using System;
using System.Collections.Generic;
using BabyDiary.DAL.FilterSearch;

namespace BabyDiary.DAL
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BabyDiaryContext context) : base(context)
        {
        }

        public User CreateUser(User user)
        {
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user;
        }

        public User FindUserBy(Filter filter)
        {
            var query = from u in ctx.Users select u;
            var deleg = ExpressionBuilder.GetExpression<User>(filter);
            return query.Where(deleg).FirstOrDefault();
        }

        public User FindUserBy(IList<Filter> filters)
        {
            var query = from u in ctx.Users select u;
            var deleg = ExpressionBuilder.GetExpression<User>(filters);
            return query.Where(deleg).FirstOrDefault();
        }

    }
}