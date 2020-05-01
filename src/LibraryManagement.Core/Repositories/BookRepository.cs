using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class BookRepository : RepositoryBase<BookEntity>
    {
        public BookRepository(DatabaseContext context) : base(context.Book, context)
        {
        }
    }
}
