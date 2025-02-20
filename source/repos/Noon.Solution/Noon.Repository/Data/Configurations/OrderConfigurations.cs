using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noon.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository.Data.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, Address => Address.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(
                orderStatus => orderStatus.ToString(),
                orderStatus => (OrderStatus) Enum.Parse(typeof(OrderStatus), orderStatus)
                );

            builder.Property(O => O.SubTotal)
                .HasColumnType("decimal(18,2)");

        }
    }
}
