using LibraryManagement.Common.Items;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Core.Mappers
{
    public class PublisherMapper : IMapper<PublisherEntity, PublisherItem>
    {
        public PublisherEntity MapToEntity(PublisherItem item, PublisherEntity sourceEntity = null)
        {
            if (sourceEntity == null)
            {
                sourceEntity = new PublisherEntity();
            }

            sourceEntity.Name = item.Name;
            sourceEntity.Id = item.Id.GetValueOrDefault();

            return sourceEntity;
        }

        public PublisherItem MapToItem(PublisherEntity entity, PublisherItem sourceItem = null)
        {
            if (sourceItem == null)
            {
                sourceItem = new PublisherItem();
            }

            sourceItem.Id = entity.Id;
            sourceItem.Name = entity.Name;

            return sourceItem;
        }
    }
}
