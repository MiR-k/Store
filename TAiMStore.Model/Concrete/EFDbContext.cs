using TAiMStore.Domain;
using System.Data.Entity;

namespace TAiMStore.Model.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
