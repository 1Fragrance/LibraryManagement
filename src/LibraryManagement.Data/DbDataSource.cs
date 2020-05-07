using LibraryManagement.Data.Repositories;
using System;

namespace LibraryManagement.Data
{
    public class DbDataSource : IDisposable
    {
        protected DatabaseContext Context { get; }

        public UserRepository Users => new UserRepository(Context);
        public BookRepository Books => new BookRepository(Context);
        public PublisherRepository Publishers => new PublisherRepository(Context);
        public AuthorRepository Authors => new AuthorRepository(Context);

        public DbDataSource(DatabaseContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
