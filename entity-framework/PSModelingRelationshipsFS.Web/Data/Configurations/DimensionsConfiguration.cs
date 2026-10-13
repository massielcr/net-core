using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public static class DimensionConfiguration
    {
        public static void ConfigureDimensions(ComplexPropertyBuilder<Dimensions> d, string prefix)
        {
            d.Property(x => x.HeightCm).HasColumnName($"{prefix}HeightCm").HasPrecision(9, 2);
            d.Property(x => x.LengthCm).HasColumnName($"{prefix}LenghtCm").HasPrecision(9, 2);
            d.Property(x => x.WidthCm).HasColumnName($"{prefix}WidthCm").HasPrecision(9, 2);
        }
    }
}
