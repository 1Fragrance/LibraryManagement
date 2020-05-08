using System;

namespace LibraryManagement.View.Modules.BusinessLogic.Admin
{
    public partial class AdminModule
    {
        public void PrintBookAssigningMenu()
        {
            Console.WriteLine("1. Выдача книги пользователю");
            Console.WriteLine("2. Вернуть книгу в библиотеку");
            Console.WriteLine("3. Назад");
        }

        public void BookAssigning()
        {
            Console.Clear();

            var exitToken = true;
            while (exitToken)
            {
                Console.Clear();
                PrintBookAssigningMenu();

                var choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();
                        
                        Console.WriteLine("Выберите пользователя");
                        var selectedUserId = SelectUserFromList();

                        Console.Clear();
                        Console.WriteLine("Выберите книгу");
                        var selectedBookId = SelectBookFromList(true);

                        BusinessService.AssignBookToUser(selectedBookId, selectedUserId);

                        PrintPressAnyBottom();
                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        Console.Clear();
                        Console.WriteLine("Выберите книгу");
                        var selectedBookId = SelectBookFromList(false);

                        BusinessService.ReturnBookToLibrary(selectedBookId);

                        break;
                    }
                    case ConsoleKey.D3:
                    {
                        Console.Clear();
                        exitToken = false;
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