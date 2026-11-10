using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSDDDCQRSInPracticeLB.Api.Entities;

namespace PSDDDCQRSInPracticeLB.Api.Data.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Street)
                   .HasMaxLength(200);

            builder.Property(a => a.City)
                   .HasMaxLength(200);

            builder.Property(a => a.State)
                   .HasMaxLength(200);

            builder.Property(a => a.PostalCode)
                   .HasMaxLength(200);

            builder.Property(a => a.Country)
                   .HasMaxLength(200);
        }
    }
}
