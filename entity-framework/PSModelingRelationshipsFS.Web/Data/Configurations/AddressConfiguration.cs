using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public static class AddressConfiguration
    {
        public static void ConfigureAddress(ComplexPropertyBuilder<Address> a, string prefix, bool required)
        {
            a.Property(x => x.Street)
             .HasColumnName($"{prefix}Street")
             .HasColumnType("varchar(200)")
             .HasMaxLength(200)
             .IsUnicode(false)
             .IsRequired(required);

            a.Property(x => x.City)
             .HasColumnName($"{prefix}City")
             .HasColumnType("varchar(100)")
             .HasMaxLength(100)
             .IsUnicode(false)
             .IsRequired(required);

            a.Property(x => x.PostalCode)
             .HasColumnName($"{prefix}PostalCode")
             .HasColumnType("varchar(20)")
             .HasMaxLength(20)
             .IsUnicode(false)
             .IsRequired(required);

            a.Property(x => x.CountryCode)
             .HasColumnName($"{prefix}CountryCode")
             .HasColumnType("char(2)")
             .IsFixedLength()
             .IsUnicode(false)
             .IsRequired(required);
        }
    }
}
