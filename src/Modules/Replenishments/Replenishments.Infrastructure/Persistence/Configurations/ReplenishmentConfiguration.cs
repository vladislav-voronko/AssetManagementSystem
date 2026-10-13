using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Replenishments.Domain.Entities;

namespace Replenishments.Infrastructure.Persistence.Configurations
{
    public class ReplenishmentConfiguration : IEntityTypeConfiguration<Replenishment>
    {
        public void Configure(EntityTypeBuilder<Replenishment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Currency)
                .HasMaxLength(10)
                .IsRequired();
        }
    }
}