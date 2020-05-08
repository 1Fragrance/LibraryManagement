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
            Console.WriteLine("4. Выдача книг пользователям");
            Console.WriteLine("5. Работа с файлами данных");
            Console.WriteLine("6. Вернутся на окно входа в учетную запись");
            Console.WriteLine("7. Выход из программы");
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
                        BookAssigning();
                        break;
                    }
                    case ConsoleKey.D5:
                    {
                        FileManagement();
                        break;
                    }
                    case ConsoleKey.D6:
                    {
                        exitToken = false;
                        Console.Clear();
                        break;
                    }
                    case ConsoleKey.D7:
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

            for (var i = 0; i < users.Count; i++)
            {
                var user = users[i];
                Console.WriteLine($"{i + 1}. {user.Login}");
            }

            var selectedUserId = ConsoleExtensions.ReadInteger(minValue: Constants.OperationConstants.ReturnOperationId, maxValue: users.Max(x => x.Id));

            if (selectedUserId == Constants.OperationConstants.ReturnOperationId)
            {
                return selectedUserId;
            }

            return users[selectedUserId - 1].Id.GetValueOrDefault();
        }

        private int SelectAuthorFromList()
        {
            var authors = BusinessService.GetAuthors();

            for (var i = 0; i < authors.Count; i++)
            {
                var author = authors[i];
                Console.WriteLine($"{i + 1}. {author.DisplayName}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(minValue: Constants.OperationConstants.ReturnOperationId, maxValue: authors.Max(x => x.Id));

            if (selectedAuthorId == Constants.OperationConstants.ReturnOperationId)
            {
                return selectedAuthorId;
            }

            return authors[selectedAuthorId - 1].Id.GetValueOrDefault();
        }

        private int SelectPublisherFromList()
        {
            var publishers = BusinessService.GetPublishers();

            for (var i = 0; i < publishers.Count; i++)
            {
                var publisher = publishers[i];
                Console.WriteLine($"{i + 1}. {publisher.Name}");
            }

            var selectedPublisherId = ConsoleExtensions.ReadInteger(minValue: Constants.OperationConstants.ReturnOperationId, maxValue: publishers.Max(x => x.Id));

            if (selectedPublisherId == Constants.OperationConstants.ReturnOperationId)
            {
                return selectedPublisherId;
            }

            return publishers[selectedPublisherId - 1].Id.GetValueOrDefault();
        }

        private int SelectBookFromList(bool? isBookInLibrary = null)
        {
            var books = isBookInLibrary != null ? BusinessService.GetBooks(isBookInLibrary.Value) : BusinessService.GetBooks();

            for (var i = 0; i < books.Count; i++)
            {
                var book = books[i];
                Console.WriteLine($"{i + 1}. {book.Name}");
            }

            var selectedBookId = ConsoleExtensions.ReadInteger(minValue: Constants.OperationConstants.ReturnOperationId, maxValue: books.Max(x => x.Id));

            if (selectedBookId == Constants.OperationConstants.ReturnOperationId)
            {
                return selectedBookId;
            }

            return books[selectedBookId - 1].Id.GetValueOrDefault();
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
