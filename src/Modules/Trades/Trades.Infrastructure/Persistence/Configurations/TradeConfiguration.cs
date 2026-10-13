using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Trades.Domain.Entities;

namespace Trades.Infrastructure.Persistence.Configurations
{
    public class TradeConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.HasKey(trade => trade.Id);
            builder.HasIndex(trade => trade.AssetId);

            builder.Property(trade => trade.AssetId).IsRequired();
            builder.Property(trade => trade.Price).IsRequired();
            builder.Property(trade => trade.Quantity).IsRequired();
            builder.Property(trade => trade.Currency)
                .HasMaxLength(10)
                .IsRequired();
            builder.Property(trade => trade.Type).IsRequired();
            builder.Property(trade => trade.ExecutedAt).IsRequired();
        }
    }
}
