using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.DataConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(p => p.Sku)
                   .HasColumnName("Sku")
                   .IsRequired()
                   .HasMaxLength(32)
                   .IsUnicode(false);

            builder.HasKey(p => p.Id)
                   .HasName("PK_Products");

            builder.HasIndex(p => p.Sku)
                   .HasDatabaseName("IX_Products_Sku");

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            builder.ToTable(t => t.HasCheckConstraint(
                    "CK_Products_Price_NonNegative",
                    "[Price] >= 0"
                ));
        }
    }
}
