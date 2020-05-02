using LibraryManagement.Core.Services;
using System;
using System.Linq;
using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Items;
using LibraryManagement.Data;

namespace LibraryManagement.View.Modules
{
    public class AdminModule : ModuleBase
    {
        private AdminService AdminService { get; }

        public AdminModule(DbDataSource dataSource)
        {
            AdminService = new AdminService(dataSource);
        }

        public void PrintMainMenu()
        {
            Console.WriteLine("Система администрирования библиотекой");
            Console.WriteLine("1. Управление учетными записями пользователей");
            Console.WriteLine("2. Управление базой данных");
            Console.WriteLine("3. Просмотр базы данных");
            Console.WriteLine("4. Работа с файлом данных");
            Console.WriteLine("5. Выход из программы");
            Console.WriteLine();
        }

        public void PrintUserManagementMenu()
        {
            Console.WriteLine("Управление учетными записями пользователей");
            Console.WriteLine("1. Просмотр всех зарегистрированных учетных записей");
            Console.WriteLine("2. Добавление новой учетной записи");
            Console.WriteLine("3. Редактирование учетной записи");
            Console.WriteLine("4. Удаление учетной записи");
            Console.WriteLine();
        }

        public void PrintFileManagementMenu()
        {
            Console.WriteLine("Работа с файлом данных");
            Console.WriteLine("1. Создание файла");
            Console.WriteLine("2. Открытие существующего файла");
            Console.WriteLine("3. Удаление файла");
            Console.WriteLine();
        }

        public void PrintBooksManagementMenu()
        {
            Console.WriteLine("Управление базой данных");
            Console.WriteLine("1. Просмотр всех добавленных книг");
            Console.WriteLine("2. Добавление новой книги в базу");
            Console.WriteLine("3. Редактирование информации о книге");
            Console.WriteLine("4. Удаление книги из базы");
            Console.WriteLine();
        }

        public void PrintBooksViewMenu()
        {
            Console.WriteLine("Просмотр базы данных");
            Console.WriteLine("1. Вывод книг с фильтрацией по заданному полю");
            Console.WriteLine("2. Вывод книг в отсортированном виде");
            Console.WriteLine("3. Вывод книг в алфавитном порядке, изданных после заданного года");
            Console.WriteLine("4. Вывод книг находящихся в текущий момент у читателей");
            Console.WriteLine();
        }

        public void WorkAsAdmin()
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
                        break;
                    }
                    case ConsoleKey.D4:
                    {
                        break;
                    }
                    case ConsoleKey.D5:
                    {
                        Environment.Exit(0);
                        break;
                    }
                }

            }
        }

        public void BooksManagement()
        {
            while (true)
            {
                Console.Clear();
                PrintBooksManagementMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();

                            var books = AdminService.GetBooksWithEverything();
                            foreach (var book in books)
                            {
                                Console.WriteLine($"Id: {book.Id}");
                                Console.WriteLine($"Регистрационный номер: {book.RegNumber}");
                                Console.WriteLine($"Название: {book.Name}");
                                Console.WriteLine($"Автор: {book.Author?.DisplayName}");
                                Console.WriteLine($"Количество страниц: {book.NumberOfPages}");
                                Console.WriteLine($"Год издания: {book.PublicationYear}");
                                Console.WriteLine($"Издательство: {book.Publisher?.Name}");
                                Console.WriteLine($"На руках у читателя: {(book.IsBookInLibrary? "Нет" : "Да" )}");
                                Console.WriteLine($"Номер читательского билета последнего читателя {book.LastUser?.LibraryCardNumber}");

                                Console.WriteLine();
                            }

                            Console.WriteLine("* Нажмите любую клавишу для выхода *");
                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            Console.WriteLine("Добавление новой книги в базу");

                            var bookItem = CreateOrUpdateBook();

                            AdminService.CreateBook(bookItem);
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            Console.WriteLine("Редактирование существующей книги");
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();

                            var bookItem = CreateOrUpdateBook();
                            bookItem.Id = selectedBookId;

                            AdminService.UpdateBook(bookItem);

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление книги из базы данных");
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();

                            AdminService.DeleteBook(selectedBookId);
                            break;
                        }
                }
            }
        }

        private BookItem CreateOrUpdateBook()
        {
            Console.WriteLine("Введите название книги:");
            var name = Console.ReadLine();

            Console.WriteLine("Введите количество страниц:");
            var countOfPages = ConsoleExtensions.ReadInteger();

            Console.WriteLine("Введите год публикации:");
            var publicationYear = ConsoleExtensions.ReadYear();

            Console.WriteLine("Введите регистрационный номер:");
            var regNumber = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine(("{0}. Добавить нового автора:", Constants.OperationConstants.AddNewSubEntityOperationId));
            Console.WriteLine(("{0}. Выбрать из существующих:", Constants.OperationConstants.SelectSubEntityOperationId));
            var authorOperationChoice = ConsoleExtensions.ReadInteger(Constants.OperationConstants.AddNewSubEntityOperationId, Constants.OperationConstants.SelectSubEntityOperationId);

            var authorItem = new AuthorItem();
            switch (authorOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие авторы:");
                    Console.WriteLine($"Нажмите \"{Constants.OperationConstants.SelectSubEntityOperationId}\" для добавления нового автора");
                    var selectedAuthorId = SelectAuthorFromList();

                    if (selectedAuthorId != Constants.OperationConstants.SelectSubEntityOperationId)
                    {
                        InputAuthorFields(authorItem);
                    }
                    else
                    {
                        authorItem.Id = selectedAuthorId;
                    }

                    break;
                }
                case Constants.OperationConstants.AddNewSubEntityOperationId:
                {
                    InputAuthorFields(authorItem);
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine(("{0}. Добавить нового издателя:", Constants.OperationConstants.AddNewSubEntityOperationId));
            Console.WriteLine(("{0}. Выбрать из существующих:", Constants.OperationConstants.SelectSubEntityOperationId));
            var publisherOperationChoice = ConsoleExtensions.ReadInteger(Constants.OperationConstants.AddNewSubEntityOperationId, Constants.OperationConstants.SelectSubEntityOperationId);

            var publisherItem = new PublisherItem();
            switch (publisherOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие издатели:");
                    Console.WriteLine($"Нажмите \"{Constants.OperationConstants.SelectSubEntityOperationId}\" для добавления нового издателя");
                    var selectedPublisherId = SelectPublisherFromList();

                    if (selectedPublisherId != Constants.OperationConstants.SelectSubEntityOperationId)
                    {
                        InputPublisherFields(publisherItem);
                    }
                    else
                    {
                        publisherItem.Id = selectedPublisherId;
                    }

                    break;
                }
                case Constants.OperationConstants.AddNewSubEntityOperationId:
                {
                    InputPublisherFields(publisherItem);
                    break;
                }
            }

            return new BookItem
            {
                Name = name,
                Author = authorItem,
                IsBookInLibrary = false,
                NumberOfPages = countOfPages,
                PublicationYear = publicationYear,
                Publisher = publisherItem,
                RegNumber = regNumber,
            };
        }

        public void UsersManagement()
        {
            while (true)
            {
                Console.Clear();
                PrintUserManagementMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();

                            var users = AdminService.GetUserListWithBooks();
                            foreach (var user in users)
                            {
                                Console.WriteLine($"Id: {user.Id}");
                                Console.WriteLine($"Логин: {user.Login}");
                                Console.WriteLine($"Роль: {user.Role.GetDescription()}");
                                Console.WriteLine($"Номер читательского билета: {user.LibraryCardNumber}");
                                Console.WriteLine($"Взятые книги: {user.LastTakenBooks?.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
                                Console.WriteLine();
                            }

                            Console.WriteLine("* Нажмите любую клавишу для выхода *");
                            Console.ReadKey();
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            Console.WriteLine("Создание новой учетной записи");

                            Console.WriteLine("Введите логин:");
                            var login = Console.ReadLine();

                            Console.WriteLine("Введите пароль:");
                            var password = Console.ReadLine();

                            Console.WriteLine("Введите уникальный 6-значный номер читательского билета или оставьте пустым для автоматической генерации");
                            var cardNumber = InputCardNumber();

                            Console.WriteLine("Тип записи: 0 - пользователь, 1 - администратор:");
                            var roleType = ConsoleExtensions.ReadRoleType();

                            var userItem = new UserItem
                            {
                                Login = login,
                                LibraryCardNumber = cardNumber,
                                Password = password,
                                Role = roleType
                            };

                            AdminService.CreateUser(userItem);
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            Console.WriteLine("Редактирование существующей учетной записи");
                            Console.WriteLine("Выберите учетную запись:");

                            var selectedUserId = SelectUserFromList();

                            Console.WriteLine("Введите новый логин:");
                            var login = Console.ReadLine();

                            Console.WriteLine("Введите новый пароль:");
                            var password = Console.ReadLine();

                            Console.WriteLine("Новый тип записи: 0 - пользователь, 1 - администратор:");
                            var roleType = ConsoleExtensions.ReadRoleType();

                            Console.WriteLine("Введите новый уникальный 6-значный номер читательского билета или оставьте пустым для автоматической генерации");
                            var cardNumber = InputCardNumber(selectedUserId);

                            var userItem = new UserItem
                            {
                                Id = selectedUserId,
                                Login = login,
                                LibraryCardNumber = cardNumber,
                                Password = password,
                                Role = roleType
                            };

                            AdminService.UpdateUser(userItem);

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление учетной записи");
                            Console.WriteLine("Выберите учетную запись:");

                            var selectedUserId = SelectUserFromList();

                            AdminService.DeleteUser(selectedUserId);
                            break;
                        }
                }
            }
        }

        private int SelectUserFromList()
        {
            var users = AdminService.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Login}");
            }

            var selectedUserId = ConsoleExtensions.ReadInteger(users.Min(x => x.Id), users.Max(x => x.Id));

            return selectedUserId;
        }

        private int SelectAuthorFromList()
        {
            var authors = AdminService.GetAuthors();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Id}. {author.DisplayName}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(authors.Min(x => x.Id), authors.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedAuthorId;
        }

        private int SelectPublisherFromList()
        {
            var publishers = AdminService.GetPublishers();

            foreach (var publisher in publishers)
            {
                Console.WriteLine($"{publisher.Id}. {publisher.Name}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(publishers.Min(x => x.Id), publishers.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedAuthorId;
        }

        private int SelectBookFromList()
        {
            var books = AdminService.GetBooks();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Id}. {book.Name}");
            }

            var selectedBookId = ConsoleExtensions.ReadInteger(books.Min(x => x.Id), books.Max(x => x.Id));

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
                    if (AdminService.IsClientCardExist(input, userId) == false)
                    {
                        return input;
                    }
                }

                Console.WriteLine("** Введенный номер карточки не подходит либо уже существует в системе **");
            }
        }

        private static void InputAuthorFields(AuthorItem authorItem)
        {
            Console.WriteLine("Введите имя автора:");
            authorItem.Name = Console.ReadLine();

            Console.WriteLine("Введите фамилию автора:");
            authorItem.Surname = Console.ReadLine();

            Console.WriteLine("Введите отчество автора:");
            authorItem.Patronymic = Console.ReadLine();
        }

        private static void InputPublisherFields(PublisherItem publisherItem)
        {
            Console.WriteLine("Введите имя издателя:");
            publisherItem.Name = Console.ReadLine();
        }
    }
}
