using Microsoft.EntityFrameworkCore;
using PSModelingRelationshipsFS.Web.DataConfigurations;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();        


        public StoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<OrderLine>(new OrderLineConfiguration());
        }
    }
}
