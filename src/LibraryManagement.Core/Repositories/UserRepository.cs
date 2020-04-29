using System.Data;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        protected override UserEntity MapRowToEntity(IDataRecord dataRecord)
        {
            throw new System.NotImplementedException();
        }
    }
}
