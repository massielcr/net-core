using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public class SalesAgentConfiguration : IEntityTypeConfiguration<SalesAgent>
    {
        public void Configure(EntityTypeBuilder<SalesAgent> builder)
        {
            builder.Property(sa => sa.DisplayName)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .IsRequired();
        }
    }
}
