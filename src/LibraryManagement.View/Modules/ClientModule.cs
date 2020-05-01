using LibraryManagement.Core.Services;
using LibraryManagement.Data;

namespace LibraryManagement.View.Modules
{
    public class ClientModule : ModuleBase
    {
        private ClientService ClientService { get; }

        public ClientModule(DbDataSource dataSource)
        {
            ClientService = new ClientService(dataSource);
        }

    }
}
