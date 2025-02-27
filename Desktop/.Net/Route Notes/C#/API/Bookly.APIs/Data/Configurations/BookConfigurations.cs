using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookly.APIs.Data.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasMany(B => B.Authors)
                .WithMany(A => A.Books)
                .UsingEntity(j=>j.ToTable("AuthorBook"));
        }
    }
}
