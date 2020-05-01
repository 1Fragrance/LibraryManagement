using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

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

        public IList<UserEntity> GetUsersIncludeLastTakenBooks()
        {
            return DbSet
                .Include(w => w.LastTakenBooks.Where(r => r.IsBookInLibrary == false))
                .ToList();
        }
    }
}
