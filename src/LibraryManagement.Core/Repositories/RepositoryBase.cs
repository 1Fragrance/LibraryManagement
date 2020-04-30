using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
    }
}
