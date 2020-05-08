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

        public bool IsLoginExist(string input, int? sourceUserId = null)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var query = DbSet.AsQueryable();

            if (sourceUserId != null)
            {
                query = query.Where(w => w.Id != sourceUserId);
            }

            return query.Any(w => w.Login == input);
        }

        public UserEntity GetUser(string login, string password)
        {
            return DbSet.FirstOrDefault(r => r.Login == login && r.Password == password);
        }

        public IList<UserEntity> GetListIncludeBooks()
        {
            return DbSet.Include(w => w.LastTakenBooks).ToList();
        }

        public IList<UserEntity> GetUsersIncludeLastTakenBooks()
        {
            return DbSet
                .Include(w => w.LastTakenBooks)
                .Where(r => r.LastTakenBooks.Any(w => w.IsBookInLibrary == false))
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

        public IList<UserEntity> GetList(int? exceptedId = null)
        {
            var query = DbSet.AsQueryable();

            if (exceptedId != null)
            {
                query = query.Where(w => exceptedId != w.Id);
            }

            return query.ToList();
        }
    }
}
