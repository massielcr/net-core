using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.Property<int>("CustomerId");
            builder.HasKey("CustomerId");

            builder.Property(cp => cp.LoyaltyTier)
                   .HasMaxLength(100)
                   .IsRequired(false);

            builder.HasOne(cp => cp.Customer)
                   .WithOne(c => c.Profile)
                   .HasForeignKey<CustomerProfile>("CustomerId")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
