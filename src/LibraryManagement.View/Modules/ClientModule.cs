using LibraryManagement.Core;
using LibraryManagement.Core.Services;

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
