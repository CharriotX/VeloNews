using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql
{
    public class WebContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public WebContext() { }

        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(c => c.NewsComments)
                .WithOne(u => u.User);

            modelBuilder.Entity<Image>()
                .HasOne(n => n.News)
                .WithMany(i => i.NewsImages);

            modelBuilder.Entity<News>()
                .HasMany(n => n.NewsComments)
                .WithOne(c => c.News);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeloNews;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}