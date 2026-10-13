using Microsoft.EntityFrameworkCore;
using PSModelingRelationshipsFS.Web.Data.Enums;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public static class ProductHierarchyConfiguration
    {
        public static void ConfigureProductHierarchy(ModelBuilder modelBuilder, InheritanceMappingStrategy strategy)
        {
            modelBuilder.Entity<PhysicalProduct>()
                .ComplexProperty(p => p.Dimensions, d => DimensionConfiguration.ConfigureDimensions(d, "Dim"));

            modelBuilder.Entity<PhysicalProduct>()
                 .Property(p => p.Manufacturer)
                 .HasColumnName("Manufacturer");

            modelBuilder.Entity<DigitalProduct>()
                 .Property(p => p.Manufacturer)
                 .HasColumnName("Manufacturer");

            switch (strategy)
            {
                case InheritanceMappingStrategy.Tph:
                    modelBuilder.Entity<Product>()
                                .ToTable("Products")
                                .HasKey(p => p.Id)
                                .HasName("PK_Products");

                    modelBuilder.Entity<Product>()
                                .ToTable(t => t.HasCheckConstraint(
                                    "CK_Products_Price_NonNegative",
                                    "[Price] >= 0"
                                ));

                    modelBuilder.Entity<Product>()
                        .HasDiscriminator<string>("ProductKind")
                        .HasValue<PhysicalProduct>(Enum.GetName(ProductKind.Physical)!)
                        .HasValue<DigitalProduct>(Enum.GetName(ProductKind.Digital)!)
                        .HasValue<SubscriptionProduct>(Enum.GetName(ProductKind.Subscription)!);

                    modelBuilder.Entity<Product>()
                                .Property("ProductKind")
                                .HasColumnName("ProductKind");
                    break;
                case InheritanceMappingStrategy.Tpt:
                    modelBuilder.Entity<Product>()
                                .ToTable("Products")
                                .UseTptMappingStrategy();

                    modelBuilder.Entity<Product>()
                                .ToTable(t => t.HasCheckConstraint(
                                    "CK_Products_Price_NonNegative",
                                    "[Price] >= 0"
                                ));

                    modelBuilder.Entity<PhysicalProduct>()
                                .ToTable("PhysicalProducts");

                    modelBuilder.Entity<DigitalProduct>()
                                .ToTable("DigitalProducts");

                    modelBuilder.Entity<SubscriptionProduct>()
                                .ToTable("SubscriptionProducts");

                    break;
                case InheritanceMappingStrategy.Tpc:
                    modelBuilder.Entity<Product>()
                                .UseTpcMappingStrategy();

                    modelBuilder.Entity<PhysicalProduct>()
                                .ToTable("PhysicalProducts", 
                                         p => p.Property(x => x.Id).UseIdentityColumn(1,3));

                    modelBuilder.Entity<DigitalProduct>()
                                .ToTable("DigitalProducts",
                                 p => p.Property(x => x.Id).UseIdentityColumn(2, 3));

                    modelBuilder.Entity<SubscriptionProduct>()
                                .ToTable("SubscriptionProducts",
                                 p => p.Property(x => x.Id).UseIdentityColumn(3, 3));
                    break;
            }

        }
    }
}
