using System.Data.Entity;

namespace AsyncAwaitProducts
{
    public class Context : DbContext
    {
        public Context() : base("DbConnection")
        {
            Database.SetInitializer<Context>(new DbInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
