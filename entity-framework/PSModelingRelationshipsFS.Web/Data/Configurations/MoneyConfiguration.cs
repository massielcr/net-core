using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSModelingRelationshipsFS.Web.Entities;

namespace PSModelingRelationshipsFS.Web.Data.Configurations
{
    public static class MoneyConfiguration
    {
        public static void ConfigureMoney(ComplexPropertyBuilder<Money> m, string amountColumn, string currencyColumn)
        {
            m.Property(x => x.Amount)
             .HasColumnName(amountColumn)
             .HasPrecision(18, 2);

            m.Property(x => x.Currency)
             .HasColumnName(currencyColumn)
             .HasColumnType("char(3)")
             .IsUnicode(false)
             .IsFixedLength()
             .HasDefaultValue("USD");
        }
    }
}
