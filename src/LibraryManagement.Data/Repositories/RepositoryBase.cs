using LibraryManagement.Common;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Data.Repositories
{
    public abstract class RepositoryBase<TEntity>  where TEntity : EntityBase
    {
        protected DatabaseContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }
        private const string LikeSymbol = "%";

        protected RepositoryBase(DbSet<TEntity> dbSet, DatabaseContext dbContext)
        {
            DbSet = dbSet;
            DbContext = dbContext;
        }

        // NOTE: Only suitable for small tables (about 1000 rows)
        public void ClearTable()
        {
            DbSet.RemoveRange(DbSet);
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
            var isRecordExist = entity.Id != Constants.DataAnnotationConstants.NewEntityId;

            DbContext.Entry(entity).State = isRecordExist ? EntityState.Modified : EntityState.Added;

            DbContext.SaveChanges();
        }

        public void SaveEntityWithId(TEntity entity)
        {
            var tableName = DbContext.Model.FindEntityType(typeof(TEntity)).GetTableName();

            DbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} ON");

            DbContext.Entry(entity).State = EntityState.Added;
            DbContext.SaveChanges();

            DbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {tableName} OFF");
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

        public string GetLikeExpression(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return LikeSymbol;
            }

            var likeExpression = $"{LikeSymbol}{str}{LikeSymbol}";
            return likeExpression;
        }
    }
}
