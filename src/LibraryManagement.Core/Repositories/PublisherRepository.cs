using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public class PublisherRepository : RepositoryBase<PublisherEntity>
    {
        public PublisherRepository(DatabaseContext context) : base(context.Publisher, context)
        {
        }
    }
}
