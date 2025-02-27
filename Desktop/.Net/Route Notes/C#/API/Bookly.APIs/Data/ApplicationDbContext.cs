using Bookly.APIs.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Bookly.APIs.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options ):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors{ get; set; }
    }
}
