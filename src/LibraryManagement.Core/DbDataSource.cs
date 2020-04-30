using LibraryManagement.Core.Repositories;

namespace LibraryManagement.Core
{
    public class DbDataSource
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
    }
}
