using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Core
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<AuthorEntity> AuthorSet { get; set; }
        public DbSet<UserEntity> UserSet { get; set; }
        public DbSet<PublisherEntity> PublisherSet { get; set; }
        public DbSet<BookEntity> BookSet { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.DatabaseConstants.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .HasOne(w => w.Author)
                .WithMany(w => w.Books)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BookEntity>()
                .HasOne(w => w.Publisher)
                .WithMany(w => w.Books)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BookEntity>()
                .HasOne(w => w.LastUser)
                .WithMany(w => w.LastTakenBooks)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
