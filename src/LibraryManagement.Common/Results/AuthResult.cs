using LibraryManagement.Common.Enums;

namespace LibraryManagement.Common.Results
{
    public class AuthResult : ResultBase
    {
        public RoleType? Role { get; set; }
    }
}
