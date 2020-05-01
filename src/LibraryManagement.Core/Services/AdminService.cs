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

        public void CreateUser(string login, string password, RoleType roleType)
        {
            var user = new UserEntity
            {
                Login = login,
                Password = password,
                Role = roleType
            };

            Context.Users.Save(user);
        }

        public void UpdateUser(int id, string login, string password, RoleType roleType)
        {
            var user = Context.Users.GetEntity(id);

            user.Login = login;
            user.Password = password;
            user.Role = roleType;

            Context.Users.Save(user);
        }

        public void DeleteUser(int id)
        {
            Context.Users.Delete(id);
        }

        public IList<UserEntity> GetUsers()
        {
            var users = Context.Users.GetList();

            return users;
        }
    }
}
