using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class AuthorRepository : RepositoryBase<AuthorEntity>
    {
        public AuthorRepository(DatabaseContext context) : base(context.Author, context)
        {
        }
    }
}
