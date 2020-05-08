using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Items;
using LibraryManagement.Core.Services.BusinessLogic;
using LibraryManagement.Core.Services.Serialization;
using LibraryManagement.Data;
using System;
using System.Data;
using System.IO;
using System.Linq;
using Console = System.Console;

namespace LibraryManagement.View.Modules.BusinessLogic
{
    public class AdminModule : BusinessLogicModuleBase<AdminService>
    {
        private FileService FileService { get; }

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
            Console.WriteLine("5. Выход из программы");
        }

        public void PrintUserManagementMenu()
        {
            Console.WriteLine("Управление учетными записями пользователей");
            Console.WriteLine("1. Просмотр всех пользователей");
            Console.WriteLine("2. Просмотр всех пользователей с книгами");
            Console.WriteLine("3. Добавление нового пользователя");
            Console.WriteLine("4. Редактирование данных о пользователе");
            Console.WriteLine("5. Удаление пользователя");
            Console.WriteLine("6. Назад");
        }

        public void PrintFileManagementMenu()
        {
            Console.WriteLine("Работа с файлом данных");
            Console.WriteLine("1. Создание файла");
            Console.WriteLine("2. Просмотр существующих файлов");
            Console.WriteLine("3. Загрузка данных из существующего файла");
            Console.WriteLine("4. Удаление файла");
            Console.WriteLine("5. Назад");
        }

        public void PrintBooksManagementMenu()
        {
            Console.WriteLine("Управление базой данных");
            Console.WriteLine("1. Добавление новой книги в базу");
            Console.WriteLine("2. Редактирование информации о книге");
            Console.WriteLine("3. Удаление книги из базы");
            Console.WriteLine("4. Назад");
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

        public void FileManagement()
        {
            Console.Clear();

            var exitToken = true;
            while (exitToken)
            {
                Console.Clear();
                PrintFileManagementMenu();

                var choice = Console.ReadKey();

                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();
                        Console.WriteLine("Создание файла");
                        Console.WriteLine("Введите название файла:");
                        var fileName = Console.ReadLine();

                        var result = FileService.CreateFile(fileName);

                        Console.WriteLine();
                        if (!result.IsSuccess)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine(error);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Файл данных \"{fileName}\" успешно создан");
                        }

                        PrintPressAnyBottom();
                        break;
                    }

                    case ConsoleKey.D2:
                    {
                        Console.Clear();
                        Console.WriteLine("Список существующих файлов");

                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId + 1}. {Path.GetFileName(fileName)}");
                            }
                        }

                        PrintPressAnyBottom();
                        break;
                    }

                    case ConsoleKey.D3:
                    {
                        Console.Clear();
                        Console.WriteLine("Загрузка данных из существующего файла");
                        Console.WriteLine("Выберите номер файла");
                        Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                        Console.WriteLine("!!! Внимание, данное действие сотрет все несохраненные данные");
                        Console.WriteLine();

                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId + 1}. {Path.GetFileName(fileName)}");
                            }

                            var fileSelection = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, fileInfos.Count);

                            if (fileSelection == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            var result = FileService.ReadFile(fileInfos[fileSelection - 1]);

                            if (!result.IsSuccess)
                            {
                                foreach (var error in result.Errors)
                                {
                                    Console.WriteLine(error);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Операция прошла успешно");
                            }
                        }

                        PrintPressAnyBottom();
                        break;
                    }

                    case ConsoleKey.D4:
                    {
                        Console.Clear();
                        Console.WriteLine("Удаление файла");
                        Console.WriteLine("Выберите номер файла");
                        Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                        Console.WriteLine();
                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId + 1}. {Path.GetFileName(fileName)}");
                            }

                            var fileSelection = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, fileInfos.Count);

                            if (fileSelection == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            var result = FileService.DeleteFile(fileInfos[fileSelection - 1]);

                            if (!result.IsSuccess)
                            {
                                foreach (var error in result.Errors)
                                {
                                    Console.WriteLine(error);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Операция прошла успешно");
                            }
                        }

                        PrintPressAnyBottom();
                        break;
                    }
                    case ConsoleKey.D5:
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

        public void BooksManagement()
        {
            Console.Clear();

            var exitToken = true;
            while (exitToken)
            {
                Console.Clear();
                PrintBooksManagementMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();
                            Console.WriteLine("Добавление новой книги в базу");

                            var bookItem = CreateOrUpdateBook();

                            BusinessService.CreateBook(bookItem);
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            Console.WriteLine("Редактирование существующей книги");
                            Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();
                            if (selectedBookId == Constants.OperationConstants.ReturnOperationId)
                            {
                                Console.Clear();
                                break;
                            }

                            var bookItem = CreateOrUpdateBook();
                            bookItem.Id = selectedBookId;

                            BusinessService.UpdateBook(bookItem);

                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление книги из базы данных");
                            Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();
                            if (selectedBookId == Constants.OperationConstants.ReturnOperationId)
                            {
                                Console.Clear();
                                break;
                            }

                            BusinessService.DeleteBook(selectedBookId);
                            break;
                        }
                    case ConsoleKey.D4:
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

        private BookItem CreateOrUpdateBook()
        {
            var bookItem = new BookItem();

            Console.WriteLine("Введите название книги:");
            bookItem.Name = Console.ReadLine();

            Console.WriteLine("Введите количество страниц:");
            bookItem.NumberOfPages = ConsoleExtensions.ReadInteger();

            Console.WriteLine("Введите год публикации:");
            bookItem.PublicationYear = ConsoleExtensions.ReadYear();

            Console.WriteLine("Введите регистрационный номер:");
            bookItem.RegNumber = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"{Constants.OperationConstants.AddNewSubEntityOperationId}. Добавить нового автора:");
            Console.WriteLine($"{Constants.OperationConstants.SelectSubEntityOperationId}. Выбрать из существующих:");
            var authorOperationChoice = ConsoleExtensions.ReadInteger(Constants.OperationConstants.AddNewSubEntityOperationId, Constants.OperationConstants.SelectSubEntityOperationId);

            var authorItem = new AuthorItem();
            switch (authorOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие авторы:");
                    Console.WriteLine($"Нажмите \"{Constants.OperationConstants.ReturnOperationId}\" для добавления нового автора");
                    var selectedAuthorId = SelectAuthorFromList();

                    if (selectedAuthorId == Constants.OperationConstants.ReturnOperationId)
                    {
                        InputAuthorFields(authorItem);
                    }
                    else
                    {
                        authorItem.Id = selectedAuthorId;
                        bookItem.AuthorId = selectedAuthorId;
                    }

                    break;
                }
                case Constants.OperationConstants.AddNewSubEntityOperationId:
                {
                    InputAuthorFields(authorItem);
                    break;
                }
            }

            bookItem.Author = authorItem;

            Console.WriteLine();
            Console.WriteLine($"{Constants.OperationConstants.AddNewSubEntityOperationId}. Добавить нового издателя:");
            Console.WriteLine($"{Constants.OperationConstants.SelectSubEntityOperationId}. Выбрать из существующих:");
            var publisherOperationChoice = ConsoleExtensions.ReadInteger(Constants.OperationConstants.AddNewSubEntityOperationId, Constants.OperationConstants.SelectSubEntityOperationId);

            var publisherItem = new PublisherItem();
            switch (publisherOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие издатели:");
                    Console.WriteLine($"Нажмите \"{Constants.OperationConstants.ReturnOperationId}\" для добавления нового издателя");
                    var selectedPublisherId = SelectPublisherFromList();

                    if (selectedPublisherId == Constants.OperationConstants.ReturnOperationId)
                    {
                        InputPublisherFields(publisherItem);
                    }
                    else
                    {
                        bookItem.PublisherId = selectedPublisherId;
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

            bookItem.Publisher = publisherItem;

            bookItem.IsBookInLibrary = false;

            return bookItem;
        }

        public void UsersManagement()
        {
            Console.Clear();

            var exitToken = true;
            while (exitToken)
            {
                PrintUserManagementMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();

                        var users = BusinessService.GetUsersIncludeBooks();
                        foreach (var user in users)
                        {
                            Console.WriteLine($"Id: {user.Id}");
                            Console.WriteLine($"Логин: {user.Login}");
                            Console.WriteLine($"Роль: {user.Role.GetDescription()}");
                            Console.WriteLine($"Номер читательского билета: {user.LibraryCardNumber}");
                            if (user.LastTakenBooks != null && user.LastTakenBooks.Any())
                            {
                                Console.WriteLine($"Последние взятые книги: {user.LastTakenBooks.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
                            }

                            Console.WriteLine();
                        }

                        PrintPressAnyBottom();
                        Console.Clear();
                        break;
                    }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();

                            var users = BusinessService.GetUserListWithBooks();
                            foreach (var user in users)
                            {
                                Console.WriteLine($"Id: {user.Id}");
                                Console.WriteLine($"Логин: {user.Login}");
                                Console.WriteLine($"Роль: {user.Role.GetDescription()}");
                                Console.WriteLine($"Номер читательского билета: {user.LibraryCardNumber}");
                                if (user.LastTakenBooks != null && user.LastTakenBooks.Any())
                                {
                                    Console.WriteLine($"Книги на руках: {user.LastTakenBooks?.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
                                }

                                Console.WriteLine();
                            }

                            PrintPressAnyBottom();
                            Console.Clear();
                            break;
                        }

                    case ConsoleKey.D3:
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

                            var result = BusinessService.CreateUser(userItem);

                            Console.Clear();
                            if (!result.IsSuccess)
                            {
                                foreach (var error in result.Errors)
                                {
                                    Console.WriteLine(error.Message);
                                }

                                Console.WriteLine();
                            }

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            Console.WriteLine("Редактирование существующей учетной записи");
                            Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                            Console.WriteLine("Выберите учетную запись:");

                            var selectedUserId = SelectUserFromList();
                            Console.Clear();
                            if (selectedUserId == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            Console.WriteLine("Введите новый логин:");
                            var login = Console.ReadLine();

                            Console.WriteLine("Введите новый пароль:");
                            var password = Console.ReadLine();

                            Console.WriteLine("Введите новый уникальный 6-значный номер читательского билета или оставьте пустым для автоматической генерации");
                            var cardNumber = InputCardNumber(selectedUserId);

                            Console.WriteLine("Новый тип записи: 0 - пользователь, 1 - администратор:");
                            var roleType = ConsoleExtensions.ReadRoleType();

                            var userItem = new UserItem
                            {
                                Id = selectedUserId,
                                Login = login,
                                LibraryCardNumber = cardNumber,
                                Password = password,
                                Role = roleType
                            };

                            var result = BusinessService.UpdateUser(userItem);

                            Console.Clear();
                            if (!result.IsSuccess)
                            {
                                foreach (var error in result.Errors)
                                {
                                    Console.WriteLine(error.Message);
                                }

                                Console.WriteLine();
                            }

                            break;
                        }

                    case ConsoleKey.D5:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление учетной записи");
                            Console.WriteLine($"Для выхода введите {Constants.OperationConstants.ReturnOperationId}");
                            Console.WriteLine("Выберите учетную запись:");

                            var selectedUserId = SelectUserFromList(CurrentUserId);
                            if (selectedUserId == Constants.OperationConstants.ReturnOperationId)
                            {
                                Console.Clear();
                                break;
                            }

                            BusinessService.DeleteUser(selectedUserId);

                            Console.Clear();
                            Console.WriteLine("Пользователь был успешно удален");

                            break;
                        }
                    case ConsoleKey.D6:
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
