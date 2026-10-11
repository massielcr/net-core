using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
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

            builder.ComplexProperty(c => c.ShippingAddress, a =>
                    AddressConfiguration.ConfigureAddress(a, "Ship", true));

            builder.ComplexProperty(c => c.BillingAddress, a =>
            {
                a.HasDiscriminator<bool>("BillingPresent")
                 .HasValue(true);

                AddressConfiguration.ConfigureAddress(a, "Bill", false);
            });

            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}
