using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.DataConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(320)
                   .IsUnicode(false);

            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
