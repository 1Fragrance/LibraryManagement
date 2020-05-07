using System;
using System.Transactions;
using LibraryManagement.Data.Repositories;

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

        public void BeginTransaction()
        {
            if (Context.Database.CurrentTransaction == null)
            {
                Context.Database.BeginTransaction();
            }
        }

        public void CommitTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                Context.Database.CommitTransaction();
            }
        }

        public void RollbackTransaction()
        {
            if (Context.Database.CurrentTransaction != null)
            {
                Context.Database.RollbackTransaction();
            }
        }

    }
}
