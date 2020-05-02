using LibraryManagement.Common.Items;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Core.Mappers
{
    public class AuthorMapper : IMapper<AuthorEntity, AuthorItem>
    {
        public AuthorEntity MapToEntity(AuthorItem item, AuthorEntity sourceEntity = null)
        {
            if (sourceEntity == null)
            {
                sourceEntity = new AuthorEntity();
            }

            sourceEntity.Name = item.Name;
            sourceEntity.Surname = item.Surname;
            sourceEntity.Patronymic = item.Patronymic;

            return sourceEntity;
        }

        public AuthorItem MapToItem(AuthorEntity entity, AuthorItem sourceItem = null)
        {
            if (sourceItem == null)
            {
                sourceItem = new AuthorItem();
            }

            sourceItem.Name = entity.Name;
            sourceItem.Patronymic = entity.Patronymic;
            sourceItem.Surname = entity.Surname;

            return sourceItem;
        }
    }
}
