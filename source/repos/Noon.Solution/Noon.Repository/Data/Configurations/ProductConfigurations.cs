using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Noon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noon.Repository.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Name).HasMaxLength(200);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");


            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(B=>B.ProductBrandId);



            builder.HasOne(P => P.Type)
                .WithMany()
                .HasForeignKey(B => B.ProductTypeId);

        }
    }
}
