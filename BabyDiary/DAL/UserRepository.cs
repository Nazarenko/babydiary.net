using BabyDiary.DAL.Interfaces;
using System.Linq;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly BabyDiaryContext _context;

        public UserRepository(BabyDiaryContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            // TODO update user class
            return user;
        }

        public User FindUserByEmail(string email)
        {
            var user = (from u in _context.Users
                           where u.Email == email
                           select u).FirstOrDefault<User>();
            return user;
        }

        public User FindUserByLogin(string login)
        {
            var user = (from u in _context.Users
                        where u.Login == login
                        select u).FirstOrDefault<User>();
            return user;
        }

        public User UpdateUser(User user)
        {
            _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            return user;

//            entities.Students.Attach(entities.Students.Single(c => c.ID == student.ID));
//            entities.Students.ApplyCurrentValues(student);
//            entities.Savechanges();
        }
    }
}