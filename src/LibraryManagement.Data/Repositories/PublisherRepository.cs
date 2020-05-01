using LibraryManagement.Data.Entities;

namespace LibraryManagement.Data.Repositories
{
    public class PublisherRepository : RepositoryBase<PublisherEntity>
    {
        public PublisherRepository(DatabaseContext context) : base(context.Publisher, context)
        {
        }
    }
}
