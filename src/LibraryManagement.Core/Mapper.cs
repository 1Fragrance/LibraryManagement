using LibraryManagement.Core.Mappers;

namespace LibraryManagement.Core
{
    public class Mapper
    {
        public AuthorMapper AuthorMapper { get; }
        public BookMapper BookMapper { get; }
        public PublisherMapper PublisherMapper { get; }
        public UserMapper UserMapper { get; }

        public Mapper()
        {
            AuthorMapper = new AuthorMapper();
            BookMapper = new BookMapper();
            PublisherMapper = new PublisherMapper();
            UserMapper = new UserMapper();
        }
    }
}
