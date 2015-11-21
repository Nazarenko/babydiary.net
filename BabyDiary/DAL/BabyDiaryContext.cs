namespace BabyDiary.DAL {
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Models.Entities;

    public partial class BabyDiaryContext : DbContext
    {
        public BabyDiaryContext()
            : base("name=BabyDiaryContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
