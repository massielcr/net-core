using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Data.Configurations
{
    public class StoreAssignmentConfiguration : IEntityTypeConfiguration<StoreAssignment>
    {
        public void Configure(EntityTypeBuilder<StoreAssignment> builder)
        {
            builder.HasKey(sa => sa.Id);

            builder.HasOne(sa => sa.Employee)
                   .WithMany(e => e.StoreAssignments)
                   .HasForeignKey(sa => sa.EmployeeId)
                   .IsRequired();

            builder.HasOne(sa => sa.Store)
                   .WithMany()
                   .HasForeignKey(sa => sa.StoreId)
                   .IsRequired();

            builder.HasOne(sa => sa.JobRole)
                   .WithMany()
                   .HasForeignKey(sa => sa.JobRoleId)
                   .IsRequired();
        }
    }
}
