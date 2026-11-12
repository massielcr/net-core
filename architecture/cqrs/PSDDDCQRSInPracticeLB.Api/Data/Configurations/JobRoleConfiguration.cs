using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Data.Configurations
{
    public class JobRoleConfiguration : IEntityTypeConfiguration<JobRole>
    {
        public void Configure(EntityTypeBuilder<JobRole> builder)
        {
            builder.HasKey(jr => jr.Id);
        }
    }
}
