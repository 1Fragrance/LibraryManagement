using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
{
    public class BookRepository : RepositoryBase<BookEntity>
    {
        public BookRepository(DatabaseContext context) : base(context.Book, context)
        {
        }

        public IList<BookEntity> GetBooksIncludeAll()
        {
            return DbSet
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher)
                .ToList();
        }
    }
}
