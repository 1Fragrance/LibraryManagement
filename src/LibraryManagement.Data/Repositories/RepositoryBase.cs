using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data.Repositories
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

        public IList<TEntity> GetList()
        {
            return DbSet.ToList();
        }

        public TEntity GetEntity(int id)
        {
            return DbSet.FirstOrDefault(w => w.Id == id);
        }

        public void Save(TEntity entity)
        {
            var isRecordExist = entity.Id != 0;

            DbContext.Entry(entity).State = isRecordExist ? EntityState.Modified : EntityState.Added;

            DbContext.SaveChanges();
        }

        public void Delete(int id)
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
