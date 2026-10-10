using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.DataConfigurations
{
    public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(ol => new { ol.OrderId, ol.ProductId });

            builder.Property(ol => ol.UnitPrice)
                   .HasPrecision(18, 2);

            builder.HasOne(ol => ol.Order)
                   .WithMany(o => o.Lines)
                   .HasForeignKey(ol => ol.OrderId);

            builder.HasOne(ol => ol.Product)
                   .WithMany()
                   .HasForeignKey(ol => ol.ProductId);

            
        }
    }
}
