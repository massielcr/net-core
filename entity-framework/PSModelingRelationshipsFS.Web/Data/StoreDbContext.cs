using Microsoft.EntityFrameworkCore;
using PSModelingRelationshipsFS.Web.Data.Configurations;
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
            modelBuilder.ApplyConfiguration<CustomerProfile>(new CustomerProfileConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
            modelBuilder.ApplyConfiguration<OrderLine>(new OrderLineConfiguration());
        }
    }
}
