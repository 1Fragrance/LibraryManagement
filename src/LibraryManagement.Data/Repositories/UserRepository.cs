using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
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

        public bool IsClientCardNumberExist(string clientCardNumber, int? userId = null)
        {
            var query = DbSet.AsQueryable();

            if (userId != null)
            {
                query = query.Where(r => r.Id != userId);
            }

            return query.Any(w => w.LibraryCardNumber == clientCardNumber);
        }
    }
}
