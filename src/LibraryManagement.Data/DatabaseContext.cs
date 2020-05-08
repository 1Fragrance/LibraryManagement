using System.Collections.Generic;
using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
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
            #if DEBUG
                 //Database.EnsureDeleted();
            #endif

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

            SeedDatabase(modelBuilder);
        }

        private static void SeedDatabase(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PublisherEntity>().HasData(new List<PublisherEntity> 
            { 
                new PublisherEntity { Id = 1, Name = "Publisher1"},
                new PublisherEntity { Id = 2, Name = "Publisher2"},
            });

            modelBuilder.Entity<AuthorEntity>().HasData(new List<AuthorEntity>
            {
                new AuthorEntity { Id = 1, Name = "AuthorName1", Surname = "AuthorSurname1", Patronymic = "AuthorPatronymic1"},
                new AuthorEntity { Id = 2, Name = "AuthorName2", Surname = "AuthorSurname2", Patronymic = "AuthorPatronymic2"},
                new AuthorEntity { Id = 3, Name = "AuthorName3", Surname = "AuthorSurname3", Patronymic = "AuthorPatronymic3"},
                new AuthorEntity { Id = 4, Name = "AuthorName4", Surname = "AuthorSurname4", Patronymic = "AuthorPatronymic4"},
                new AuthorEntity { Id = 5, Name = "AuthorName5", Surname = "AuthorSurname5", Patronymic = "AuthorPatronymic5"},
            });

            modelBuilder.Entity<UserEntity>().HasData(new List<UserEntity>
            {
                new UserEntity { Id = 1, Login = "Login1", Password = "123", Role = RoleType.Admin, LibraryCardNumber = "111111"},
                new UserEntity { Id = 2, Login = "Login2", Password = "123", Role = RoleType.Client, LibraryCardNumber = "222222"},
                new UserEntity { Id = 3, Login = "Login3", Password = "123", Role = RoleType.Admin, LibraryCardNumber = "333333"},
            });

            modelBuilder.Entity<BookEntity>().HasData(new List<BookEntity>
            {
                new BookEntity { Id = 1, RegNumber = "1", Name = "Name1", NumberOfPages = 1, PublicationYear = 2001, IsBookInLibrary = true,  PublisherId = 1, AuthorId = 1, LastUserId = null},
                new BookEntity { Id = 2, RegNumber = "2", Name = "Name2", NumberOfPages = 2, PublicationYear = 2002, IsBookInLibrary = false, PublisherId = 2, AuthorId = 2, LastUserId = 1},
                new BookEntity { Id = 3, RegNumber = "3", Name = "Name3", NumberOfPages = 3, PublicationYear = 2003, IsBookInLibrary = true,  PublisherId = null, AuthorId = 3, LastUserId = 2},
                new BookEntity { Id = 4, RegNumber = "4", Name = "Name4", NumberOfPages = 4, PublicationYear = 2004, IsBookInLibrary = false, PublisherId = 1, AuthorId = 4, LastUserId = 3},
                new BookEntity { Id = 5, RegNumber = "5", Name = "Name5", NumberOfPages = 5, PublicationYear = 2005, IsBookInLibrary = true,  PublisherId = 2, AuthorId = 5, LastUserId = null},
                new BookEntity { Id = 6, RegNumber = "6", Name = "Name6", NumberOfPages = 6, PublicationYear = 2006, IsBookInLibrary = false, PublisherId = null, AuthorId = null, LastUserId = 1},
                new BookEntity { Id = 7, RegNumber = "7", Name = "Name7", NumberOfPages = 7, PublicationYear = 2007, IsBookInLibrary = true,  PublisherId = 1, AuthorId = 2, LastUserId = 2},
                new BookEntity { Id = 8, RegNumber = "8", Name = "Name8", NumberOfPages = 8, PublicationYear = 2008, IsBookInLibrary = false, PublisherId = 2, AuthorId = 3, LastUserId = 3}
            });
        }
    }
}
