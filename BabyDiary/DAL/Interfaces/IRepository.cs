
using System.Collections.Generic;
using System.Threading.Tasks;
using BabyDiary.DAL.FilterSearch;
using BabyDiary.Models.Entities;

namespace BabyDiary.DAL.Interfaces
{
    public interface IRepository<out T>
    {
        Task SaveChangesAsync();

    }
}
