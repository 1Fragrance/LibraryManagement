using LibraryManagement.Core.Entities;
using System.Collections.Generic;
using LibraryManagement.Core.Enums;

namespace LibraryManagement.Core.Services
{
    public class AdminService : ServiceBase
    {
        public AdminService(DbDataSource context) : base(context)
        {
        }

        public IList<UserEntity> GetUserListWithBooks()
        {
            var users = Context.Users.GetUsersIncludeLastTakenBooks();

            return users;
        }

        public void CreateUser(string login, string password, int roleTypeId)
        {
            var user = new UserEntity()
            {
                Login = login,
                Password = password,
                Role = (RoleType) roleTypeId
            };

            Context.Users.Save(user);
        }

        public IList<UserEntity> GetUsers()
        {
            var users = Context.Users.GetList();

            return users;
        }
    }
}
