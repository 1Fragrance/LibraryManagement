using System.Linq;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(DatabaseContext context) : base(context.User, context)
        {
        }

        public UserEntity GetUser(string login, string password)
        {
            return DbSet.FirstOrDefault(r => r.Login == login && r.Password == password);
        }
    }
}
