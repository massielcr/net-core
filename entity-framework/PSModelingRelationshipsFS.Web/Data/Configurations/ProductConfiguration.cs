using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id)
                   .HasName("PK_Products");

            builder.Property(p => p.Sku)
                   .HasColumnName("Sku")
                   .IsRequired()
                   .HasMaxLength(32)
                   .IsUnicode(false);          

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200)
                   .IsUnicode(false);

            builder.ComplexProperty(p => p.Price, m => 
                    MoneyConfiguration.ConfigureMoney(m, "Price", "PriceCurrency"));

            builder.ToTable(t => t.HasCheckConstraint(
                    "CK_Products_Price_NonNegative",
                    "[Price] >= 0"
                ));

            builder.HasIndex(p => p.Sku)
                   .HasDatabaseName("IX_Products_Sku");

            builder.HasMany(p => p.Tags)
                   .WithMany(t => t.Products)
                   .UsingEntity<Dictionary<string, object>>(
                    "ProductTag",
                    right => right.HasOne<Tag>()
                                  .WithMany()
                                  .HasForeignKey("TagId")
                                  .OnDelete(DeleteBehavior.Cascade),
                    left => left.HasOne<Product>()
                                .WithMany()
                                .HasForeignKey("ProductId")
                                .OnDelete(DeleteBehavior.Cascade),
                    join =>
                    {
                        join.ToTable("ProductTags");
                        join.HasKey("ProductId", "TagId");
                        join.HasIndex("TagId");
                    });
        }
    }
}
