using LibraryManagement.Common;
using LibraryManagement.Common.Items;
using LibraryManagement.Core.Services.BusinessLogic;
using LibraryManagement.Core.Services.Serialization;
using LibraryManagement.Data;
using System;
using System.Linq;
using Console = System.Console;

namespace LibraryManagement.View.Modules.BusinessLogic.Admin
{
    public partial class AdminModule : BusinessLogicModuleBase<AdminService>
    {
        public AdminModule(int currentUserId, DbDataSource dataSource) : base(currentUserId)
        {
            BusinessService = new AdminService(dataSource);
            FileService = new FileService(dataSource);
        }

        public override void PrintMainMenu()
        {
            Console.WriteLine("Система администрирования библиотекой");
            Console.WriteLine("1. Управление учетными записями пользователей");
            Console.WriteLine("2. Управление книгами");
            Console.WriteLine("3. Просмотр книг");
            Console.WriteLine("4. Работа с файлами данных");
            Console.WriteLine("5. Вернутся на окно входа в учетную запись");
            Console.WriteLine("6. Выход из программы");
        }

        public void WorkAsAdmin()
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
                        UsersManagement();
                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        BooksManagement();
                        break;
                    }
                    case ConsoleKey.D3:
                    {
                        BooksView();
                        break;
                    }
                    case ConsoleKey.D4:
                    {
                        FileManagement();
                        break;
                    }
                    case ConsoleKey.D5:
                    {
                        exitToken = false;
                        Console.Clear();
                        break;
                    }
                    case ConsoleKey.D6:
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
            // ReSharper disable once FunctionNeverReturns
        }

        private int SelectUserFromList(int? exceptedId = null)
        {
            var users = BusinessService.GetUsers(exceptedId);

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Login}");
            }

            var selectedUserId = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, users.Max(x => x.Id));

            return selectedUserId;
        }

        private int SelectAuthorFromList()
        {
            var authors = BusinessService.GetAuthors();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Id}. {author.DisplayName}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, authors.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedAuthorId;
        }

        private int SelectPublisherFromList()
        {
            var publishers = BusinessService.GetPublishers();

            foreach (var publisher in publishers)
            {
                Console.WriteLine($"{publisher.Id}. {publisher.Name}");
            }

            var selectedPublisherId = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, publishers.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedPublisherId;
        }

        private int SelectBookFromList()
        {
            var books = BusinessService.GetBooks();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Name}");
            }

            var selectedBookId = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, books.Max(x => x.Id));

            return selectedBookId;
        }


        private string InputCardNumber(int? userId = null)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }

                if (input.Length == Constants.DataAnnotationConstants.LibraryCardNumberMaxLength)
                {
                    if (BusinessService.IsClientCardExist(input, userId) == false)
                    {
                        return input;
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Введенный номер карточки не подходит либо уже существует в системе");
            }
        }

        private static void InputAuthorFields(BookItem book)
        {
            var authorItem = new AuthorItem();

            Console.WriteLine("Введите имя автора:");
            authorItem.Name = ConsoleExtensions.ReadNotEmptyString();

            Console.WriteLine("Введите фамилию автора:");
            authorItem.Surname = ConsoleExtensions.ReadNotEmptyString();

            Console.WriteLine("Введите отчество автора:");
            authorItem.Patronymic = ConsoleExtensions.ReadNotEmptyString();

            book.Author = authorItem;
        }

        private static void InputPublisherFields(BookItem book)
        {
            var publisherItem = new PublisherItem();

            Console.WriteLine("Введите имя издателя:");
            publisherItem.Name = ConsoleExtensions.ReadNotEmptyString();

            book.Publisher = publisherItem;
        }
    }
}
