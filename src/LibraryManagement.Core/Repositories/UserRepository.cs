using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(DatabaseContext context) : base(context.UserSet, context)
        {
        }
    }
}
