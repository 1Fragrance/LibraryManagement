using System.Data;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class BookRepository : RepositoryBase<BookEntity>
    {
        public BookRepository(DatabaseContext context) : base(context)
        {
        }

        protected override BookEntity MapRowToEntity(IDataRecord dataRecord)
        {
            throw new System.NotImplementedException();
        }
    }
}
