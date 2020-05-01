using LibraryManagement.Core.Enums;

namespace LibraryManagement.Core.Results
{
    public class AuthResult : ResultBase
    {
        public RoleType? Role { get; set; }
    }
}
