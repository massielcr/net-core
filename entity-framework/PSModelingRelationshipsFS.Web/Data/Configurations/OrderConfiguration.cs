using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ComplexProperty(o => o.ShipTo, st => 
                    AddressConfiguration.ConfigureAddress(st, "ShipTo", true));

            builder.HasOne(o => o.SalesAgent)
                   .WithMany(sa => sa.Orders)
                   .HasForeignKey("SalesAgentId")
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
