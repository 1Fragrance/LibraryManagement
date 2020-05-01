using LibraryManagement.Common.Results;
using LibraryManagement.Data;

namespace LibraryManagement.Core.Services
{
    public class AuthService : ServiceBase
    {
        public AuthService(DbDataSource context) : base(context)
        {
        }

        public AuthResult SignIn(string login, string password)
        {
            //TODO: validation 

            var userEntity = Context.Users.GetUser(login, password);

            if (userEntity == null)
            {
                return new AuthResult { IsSuccess = false };
            }

            return new AuthResult
            {
                IsSuccess = true,
                Role = userEntity.Role
            };
        }
    }
}
