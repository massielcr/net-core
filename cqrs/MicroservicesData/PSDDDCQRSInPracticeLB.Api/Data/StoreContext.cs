using Microsoft.EntityFrameworkCore;
using PSDDDCQRSInPracticeLB.Api.Entities;
using System.Reflection;

namespace PSDDDCQRSInPracticeLB.Api.Data
{
    public class StoreContext : DbContext
    {
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<EmployeeSalary> EmployeeSalaries => Set<EmployeeSalary>();
        public DbSet<JobRole> JobRoles => Set<JobRole>();
        public DbSet<Store> Stores => Set<Store>();                     
        public DbSet<StoreAssignment> StoreAssignments => Set<StoreAssignment>();

        public StoreContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = 1, Name = "Bethany's Pie Shop - Brussels", Location = "Grote Markt" },
                new Store { Id = 2, Name = "Bethany's Pie Shop - Antwerp", Location = "Meir" },
                new Store { Id = 3, Name = "Bethany's Pie Shop - Leuven", Location = "Oude Markt" }
            );

            modelBuilder.Entity<JobRole>().HasData(
                new JobRole { Id = 1, Name = "Baker" },
                new JobRole { Id = 2, Name = "Cashier" },
                new JobRole { Id = 3, Name = "Manager" }
            );
        }
    }
}
