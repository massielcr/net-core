using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Data.Configurations
{
    public class EmployeeSalaryConfiguration : IEntityTypeConfiguration<EmployeeSalary>
    {
        public void Configure(EntityTypeBuilder<EmployeeSalary> builder)
        {
            builder.HasKey(es => es.Id);

            builder.HasOne(es => es.Employee)
                   .WithMany(e => e.Salaries)
                   .HasForeignKey(es => es.EmployeeId)
                   .IsRequired();

        }
    }
}
