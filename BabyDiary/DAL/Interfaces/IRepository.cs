
using System.Collections.Generic;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IRepository<out T>
    {
        void SaveChanges();

    }
}
