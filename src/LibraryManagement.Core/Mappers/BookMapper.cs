using LibraryManagement.Common.Items;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Core.Mappers
{
    public class BookMapper : IMapper<BookEntity, BookItem>
    {
        public BookEntity MapToEntity(BookItem item, BookEntity sourceEntity = null)
        {
            if (sourceEntity == null)
            {
                sourceEntity = new BookEntity();
            }

            sourceEntity.Id = item.Id.GetValueOrDefault();
            sourceEntity.Name = item.Name;
            sourceEntity.PublicationYear = item.PublicationYear;
            sourceEntity.NumberOfPages = item.NumberOfPages;
            sourceEntity.IsBookInLibrary = item.IsBookInLibrary;
            sourceEntity.RegNumber = item.RegNumber;
            sourceEntity.PublisherId = item.PublisherId;
            sourceEntity.AuthorId = item.AuthorId;
            sourceEntity.LastUserId = item.AuthorId;

            if (item.Publisher != null)
            {
                sourceEntity.Publisher = new PublisherEntity()
                {
                    Id = item.Publisher.Id.GetValueOrDefault(),
                    Name = item.Publisher.Name,
                };
            }

            if (item.Author != null)
            {
                sourceEntity.Author = new AuthorEntity
                {
                    Id = item.Author.Id.GetValueOrDefault(),
                    Name = item.Author.Name,
                    Patronymic = item.Author.Patronymic,
                    Surname = item.Author.Patronymic
                };
            }

            if (item.LastUser != null)
            {
                sourceEntity.LastUser = new UserEntity
                {
                    Login = item.LastUser.Login,
                    Id = item.LastUser.Id.GetValueOrDefault(),
                    LibraryCardNumber = item.LastUser.LibraryCardNumber,
                    Role = item.LastUser.Role,
                };
            }

            return sourceEntity;
        }

        public BookItem MapToItem(BookEntity entity, BookItem sourceItem = null)
        {
            if (sourceItem == null)
            {
                sourceItem = new BookItem();
            }

            sourceItem.Id = entity.Id;
            sourceItem.Name = entity.Name;
            sourceItem.PublicationYear = entity.PublicationYear;
            sourceItem.NumberOfPages = entity.NumberOfPages;
            sourceItem.IsBookInLibrary = entity.IsBookInLibrary;
            sourceItem.RegNumber = entity.RegNumber;
            sourceItem.PublisherId = entity.PublisherId;
            sourceItem.AuthorId = entity.AuthorId;
            sourceItem.LastUserId = entity.AuthorId;

            if (entity.Publisher != null)
            {
                sourceItem.Publisher = new PublisherItem
                {
                    Id = entity.Publisher.Id,
                    Name = entity.Publisher.Name,
                };
            }

            if (entity.Author != null)
            {
                sourceItem.Author = new AuthorItem
                {
                    Id = entity.Author.Id,
                    Name = entity.Author.Name,
                    Patronymic = entity.Author.Patronymic,
                    Surname = entity.Author.Patronymic
                };
            }

            if (entity.LastUser != null)
            {
                sourceItem.LastUser = new UserItem
                {
                    Login = entity.LastUser.Login,
                    Id = entity.LastUser.Id,
                    LibraryCardNumber = entity.LastUser.LibraryCardNumber,
                    Role = entity.LastUser.Role,
                };
            }

            return sourceItem;
        }
    }
}
