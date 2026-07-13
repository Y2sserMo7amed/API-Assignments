using ECommerceApp.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Price).HasColumnType("decimal(8,2)");

            // ProductItemOrdered is also an Owned Entity - its columns are in OrderItems
            builder.OwnsOne(oi => oi.ProductItemOrdered, product =>
            {
                product.Property(p => p.ProductName).HasMaxLength(100);
                product.Property(p => p.PictureUrl).HasMaxLength(200);
            });
        }
    }
}
