using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public class ProductSuplierConfiguration : IEntityTypeConfiguration<ProductSuplier>
    {
        public void Configure(EntityTypeBuilder<ProductSuplier> builder)
        {
            builder.ToTable("ProductSupliers");

            builder.HasKey(ps => new { ps.ProductId, ps.SuplierId });

            builder.Property(ps => ps.ContractPrice)
                   .HasPrecision(18, 2);

            builder.Property(ps => ps.LeadTimeDays);
            builder.Property(ps => ps.IsPreferred);

            builder.HasOne(ps => ps.Product)
                   .WithMany(p => p.Supliers)
                   .HasForeignKey(ps => ps.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ps => ps.Suplier)
                   .WithMany(s => s.Products)
                   .HasForeignKey(ps => ps.SuplierId)
                   .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
