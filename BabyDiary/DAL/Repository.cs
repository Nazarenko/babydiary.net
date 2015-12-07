using System.Threading.Tasks;
using BabyDiary.DAL.Interfaces;

namespace BabyDiary.DAL
{
    public class Repository<T> : IRepository<T>
    {
        protected readonly BabyDiaryContext ctx;

        public Repository(BabyDiaryContext context)
        {
            ctx = context;
        }

        public async Task SaveChangesAsync()
        {
            await ctx.SaveChangesAsync();
        }
    }
}