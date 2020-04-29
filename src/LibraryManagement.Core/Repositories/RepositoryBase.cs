using System.Collections.Generic;
using System.Data;
using LibraryManagement.Core.Entities;

namespace LibraryManagement.Core.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected DatabaseContext Context { get; set; }

        protected RepositoryBase(DatabaseContext context)
        {
            Context = context;
        }

        protected IList<TEntity> GetList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                var entityList = new List<TEntity>();
                while (reader.Read())
                {
                    var entity = MapRowToEntity(reader);
                    entityList.Add(entity);
                }

                return entityList;
            }
        }

        protected abstract TEntity MapRowToEntity(IDataRecord dataRecord);
    }
}
