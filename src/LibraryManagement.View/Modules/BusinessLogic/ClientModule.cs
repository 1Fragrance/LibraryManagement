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
            Console.WriteLine("2. Вернутся на окно входа в учетную запись");
            Console.WriteLine("3. Выход из программы");
        }

        public void WorkAsClient()
        {
            Console.Clear();
            var exitToken = true;
            while (exitToken)
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
                        exitToken = false;
                        Console.Clear();
                        break;
                    }
                    case ConsoleKey.D3:
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
