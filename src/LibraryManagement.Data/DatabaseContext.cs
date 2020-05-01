using LibraryManagement.Common;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<AuthorEntity> Author { get; set; }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<PublisherEntity> Publisher { get; set; }
        public DbSet<BookEntity> Book { get; set; }

        public DatabaseContext()
        {
            Database.EnsureDeleted();
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
