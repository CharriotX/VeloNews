using Data.Interface.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Sql
{
    public class WebContext : DbContext
    {
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfileImage> UserProfileImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<UserActivityHub> UserActivities { get; set; }

        public WebContext() { }

        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.News)
                .WithOne(n => n.Creator);

            modelBuilder.Entity<User>()
                .HasMany(c => c.NewsComments)
                .WithOne(u => u.User);

            modelBuilder.Entity<NewsImage>()
                .HasOne(i => i.News)
                .WithMany(n => n.NewsImages);

            modelBuilder.Entity<News>()
                .HasMany(n => n.NewsComments)
                .WithOne(c => c.News);

            modelBuilder.Entity<UserProfileImage>()
                .HasOne(i => i.User)
                .WithOne(u => u.UserProfileImage)
                .HasForeignKey<UserProfileImage>("UserId")
                .IsRequired();

            modelBuilder.Entity<News>()
                .HasOne(n => n.Category)
                .WithMany(c => c.News)
                .HasForeignKey("CategoryId")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=VeloNews;Integrated Security=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}