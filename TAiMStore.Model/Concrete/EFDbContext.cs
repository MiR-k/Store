using System;
using System.Data.Entity;
using System.Linq;
using TAiMStore.Domain;

namespace TAiMStore.Model.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public override int SaveChanges()
        {
            foreach (var source in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                source.Property("CreateDate").CurrentValue = DateTime.Now;
            }
            foreach (var source in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
            {
                source.Property("ModifiedDate").CurrentValue = DateTime.Now;
            }
            return base.SaveChanges();
        }

        public virtual void Commit()
        {
            SaveChanges();
        }
    }
}
