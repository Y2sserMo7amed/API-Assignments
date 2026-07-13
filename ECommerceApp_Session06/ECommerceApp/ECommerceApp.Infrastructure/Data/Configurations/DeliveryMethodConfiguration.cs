using ECommerceApp.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Infrastructure.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasMaxLength(50);
        }
    }
}
