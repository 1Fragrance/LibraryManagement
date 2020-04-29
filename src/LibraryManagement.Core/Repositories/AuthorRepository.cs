using System.Data;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class AuthorRepository : RepositoryBase<AuthorEntity>
    {
        public AuthorRepository(DatabaseContext context) : base(context)
        {
        }

        protected override AuthorEntity MapRowToEntity(IDataRecord dataRecord)
        {
            throw new System.NotImplementedException();
        }
    }
}
