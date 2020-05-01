using LibraryManagement.Data.Entities;

namespace LibraryManagement.Data.Repositories
{
    public class AuthorRepository : RepositoryBase<AuthorEntity>
    {
        public AuthorRepository(DatabaseContext context) : base(context.Author, context)
        {
        }
    }
}
