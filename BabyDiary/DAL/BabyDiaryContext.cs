namespace BabyDiary.DAL {
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models.Entities;

    public partial class BabyDiaryContext : DbContext
    {
        public BabyDiaryContext()
            : base("name=BabyDiaryContext")
        {
        }

        static BabyDiaryContext()
        {
            Database.SetInitializer<BabyDiaryContext>(null);
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).Enabled = true;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}
