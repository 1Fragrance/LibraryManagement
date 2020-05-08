using LibraryManagement.Common.Items;
using LibraryManagement.Data.Entities;
using System.Linq;

namespace LibraryManagement.Core.Mappers
{
    public class UserMapper : IMapper<UserEntity, UserItem>
    {
        public UserEntity MapToEntity(UserItem item, UserEntity sourceEntity = null)
        {
            if (sourceEntity == null)
            {
                sourceEntity = new UserEntity();
            }

            sourceEntity.Id = item.Id.GetValueOrDefault();
            sourceEntity.Login = item.Login;
            sourceEntity.Password = item.Password;
            sourceEntity.Role = item.Role;
            sourceEntity.LibraryCardNumber = item.LibraryCardNumber;

            if (item.LastTakenBooks != null)
            {
                sourceEntity.LastTakenBooks = item.LastTakenBooks?.Select(w => new BookEntity
                {
                    Id = w.Id.GetValueOrDefault(),
                    IsBookInLibrary = w.IsBookInLibrary,
                    Name = w.Name,
                    NumberOfPages = w.NumberOfPages,
                    PublicationYear = w.PublicationYear,
                    RegNumber = w.RegNumber
                }).ToList();
            }

            return sourceEntity;
        }


        public UserItem MapToItem(UserEntity entity, UserItem sourceItem = null)
        {
            if (sourceItem == null)
            {
                sourceItem = new UserItem();
            }

            sourceItem.Login = entity.Login;
            sourceItem.LibraryCardNumber = entity.LibraryCardNumber;
            sourceItem.Role = entity.Role;
            sourceItem.Id = entity.Id;
            sourceItem.LastTakenBooks = entity.LastTakenBooks?.Select(w => new BookItem
            {
                Id = w.Id,
                IsBookInLibrary = w.IsBookInLibrary,
                Name = w.Name,
                NumberOfPages = w.NumberOfPages,
                PublicationYear = w.PublicationYear,
                RegNumber = w.RegNumber
            }).ToList();

            return sourceItem;
        }
    }
}
