using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Core
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AuthorEntity> AuthorSet { get; set; }
        public DbSet<UserEntity> UserSet { get; set; }
        public DbSet<PublisherEntity> PublisherSet { get; set; }
        public DbSet<BookEntity> BookSet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Constants.ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
