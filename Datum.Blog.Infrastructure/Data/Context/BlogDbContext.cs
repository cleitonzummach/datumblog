using Datum.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.Infrastructure.Data.Context
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }
        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Publicacao> Publicacao { get; set; }
    }
}
