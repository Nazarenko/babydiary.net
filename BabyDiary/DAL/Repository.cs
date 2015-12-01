using System;
using System.Collections.Generic;
using System.Linq;
using BabyDiary.DAL.FilterSearch;
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

        public void SaveChanges()
        {
            ctx.SaveChanges();
        }
    }
}