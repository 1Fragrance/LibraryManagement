using LibraryManagement.Data;

namespace LibraryManagement.Core.Services
{
    public abstract class ServiceBase
    {
        protected DbDataSource Context { get; }

        protected ServiceBase(DbDataSource context)
        {
            this.Context = context;
        }
    }
}
