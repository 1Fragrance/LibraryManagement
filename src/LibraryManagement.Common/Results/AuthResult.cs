using LibraryManagement.Common.Enums;

namespace LibraryManagement.Common.Results
{
    public class AuthResult : ExecutionResult
    {
        public RoleType? Role { get; set; }
    }
}
