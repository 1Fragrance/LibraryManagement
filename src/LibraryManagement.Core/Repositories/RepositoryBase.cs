using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Core.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected DatabaseContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected RepositoryBase(DbSet<TEntity> dbSet, DatabaseContext dbContext)
        {
            DbSet = dbSet;
            DbContext = dbContext;
        }

        protected IList<TEntity> GetList()
        {
            return DbSet.ToList();
        }

        protected TEntity GetEntity(int id)
        {
            return DbSet.FirstOrDefault(w => w.Id == id);
        }

        protected void Save(TEntity entity)
        {
            var isRecordExist = entity.Id != 0;

            DbContext.Entry(entity).State = isRecordExist ? EntityState.Modified : EntityState.Added;

            DbContext.SaveChanges();
        }

        protected void Delete(int id)
        {
            var entity = DbSet.Find(id);

            if (entity != null)
            {
                DbContext.Entry(entity).State = EntityState.Deleted;
                DbContext.SaveChanges();
            }
        }
    }
}
