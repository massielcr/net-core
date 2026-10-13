using Microsoft.EntityFrameworkCore;
using PSModelingRelationshipsFS.Web.Data.Configurations;
using PSModelingRelationshipsFS.Web.Data.Enums;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<PhysicalProduct> PhysicalProducts => Set<PhysicalProduct>();
        public DbSet<DigitalProduct> DigitalProducts => Set<DigitalProduct>();
        public DbSet<SubscriptionProduct> SubscriptionProductProducts => Set<SubscriptionProduct>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderLine> OrderLines => Set<OrderLine>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Suplier> Supliers => Set<Suplier>();
        public DbSet<ProductSuplier> ProductSupliers => Set<ProductSuplier>();
        public DbSet<SalesAgent> SalesAgents => Set<SalesAgent>();
        public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();


        public StoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ProductHierarchyConfiguration.ConfigureProductHierarchy(modelBuilder, InheritanceMappingStrategy.Tph);

            modelBuilder.ApplyConfiguration<Customer>(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration<CustomerProfile>(new CustomerProfileConfiguration());            
            modelBuilder.ApplyConfiguration<Order>(new OrderConfiguration());
            modelBuilder.ApplyConfiguration<OrderLine>(new OrderLineConfiguration());
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<ProductSuplier>(new ProductSuplierConfiguration());
            modelBuilder.ApplyConfiguration<SalesAgent>(new SalesAgentConfiguration());
            modelBuilder.ApplyConfiguration<Suplier>(new SuplierConfiguration());
            modelBuilder.ApplyConfiguration<Tag>(new TagConfiguration());            
        }
    }
}
