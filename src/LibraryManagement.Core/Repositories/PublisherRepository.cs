using System.Data;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class PublisherRepository : RepositoryBase<PublisherEntity>
    {
        public PublisherRepository(DatabaseContext context) : base(context)
        {
        }

        protected override PublisherEntity MapRowToEntity(IDataRecord dataRecord)
        {
            throw new System.NotImplementedException();
        }
    }
}
