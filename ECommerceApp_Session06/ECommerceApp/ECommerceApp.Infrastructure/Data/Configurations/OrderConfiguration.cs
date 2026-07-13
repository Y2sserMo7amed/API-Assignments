using ECommerceApp.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.SubTotal).HasColumnType("decimal(8,2)");

            // ShippingAddress is an "Owned Entity" - its columns are stored
            // directly inside the Orders table (no separate AddressTable).
            builder.OwnsOne(o => o.ShippingAddress, address =>
            {
                address.Property(a => a.FirstName).HasMaxLength(50);
                address.Property(a => a.LastName).HasMaxLength(50);
                address.Property(a => a.Street).HasMaxLength(50);
                address.Property(a => a.City).HasMaxLength(50);
                address.Property(a => a.Country).HasMaxLength(50);
            });

            // Store the enum as a readable string ("Pending") not a number (0)
            builder.Property(o => o.Status)
                .HasConversion(
                    status => status.ToString(),
                    value => (OrderStatus)Enum.Parse(typeof(OrderStatus), value));

            // When an Order is deleted, delete all its OrderItems too
            builder.HasMany(o => o.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
