using lesson1_Simple_Functions___Controller.Models;
using lesson1_Simple_Functions___Controller.Models.Filters;

namespace lesson1_Simple_Functions___Controller.Dataa
{
    public class PostgreSqlContext: DbContext
    {
        /*public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*base.OnConfiguring(optionsBuilder);*/
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=shopdb;Username=postgres;Password=12345678");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var addedEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added).Select(e => e.Entity);

            foreach(var entry in addedEntries)
            {
                var item = entry as BaseEntity;

                if(item != null)
                    item.CreatedDate = DateTime.UtcNow;
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified).Select(e => e.Entity);

            foreach (var entry in modifiedEntries)
            {
                var item = entry as BaseEntity;

                if (item != null)
                    item.UpdatedDate = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Brand> Brands { get; set; } = null!;
        public DbSet<Color> Colors { get; set; } = null!;

    }
}
