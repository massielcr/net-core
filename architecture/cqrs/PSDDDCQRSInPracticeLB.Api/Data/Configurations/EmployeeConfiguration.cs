using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Address)
                   .WithOne()
                   .HasForeignKey<Employee>(e => e.AddressId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
