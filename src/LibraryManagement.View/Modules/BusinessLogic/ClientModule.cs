using LibraryManagement.Core.Services.BusinessLogic;
using LibraryManagement.Data;
using System;

namespace LibraryManagement.View.Modules.BusinessLogic
{
    public class ClientModule : BusinessLogicModuleBase<ClientService>
    {
        public ClientModule(int currentUserId, DbDataSource dataSource) : base(currentUserId)
        {
            BusinessService = new ClientService(dataSource);
        }

        public override void PrintMainMenu()
        {
            Console.WriteLine("Система просмотра наличия книг в библиотеке");
            Console.WriteLine("1. Просмотр книг");
            Console.WriteLine("2. Выход из программы");
        }

        public void WorkAsClient()
        {
            Console.Clear();

            while (true)
            {
                PrintMainMenu();
                var choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        BooksView();
                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                    {
                        Console.Clear();
                        break;
                    }
                }
            }
        }
    }
}
