using LibraryManagement.Common.Items;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Core.Mappers
{
    public interface IMapper<TEntity, TItem> where TEntity : EntityBase where TItem : ItemBase
    {
        public TEntity MapToEntity(TItem item, TEntity sourceEntity = null);

        public TItem MapToItem(TEntity entity, TItem sourceItem = null);
    }
}
