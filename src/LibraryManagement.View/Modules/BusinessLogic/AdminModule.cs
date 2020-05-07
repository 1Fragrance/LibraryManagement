using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Items;
using LibraryManagement.Core.Services.BusinessLogic;
using LibraryManagement.Core.Services.Serialization;
using LibraryManagement.Data;
using System;
using System.Linq;
using Console = System.Console;

namespace LibraryManagement.View.Modules.BusinessLogic
{
    public class AdminModule : BusinessLogicModuleBase<AdminService>
    {
        private FileService FileService { get; }

        public AdminModule(DbDataSource dataSource)
        {
            BusinessService = new AdminService(dataSource);
            FileService = new FileService(dataSource);
        }

        public override void PrintMainMenu()
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
            Console.WriteLine("5. Назад");
            Console.WriteLine();
        }

        public void PrintFileManagementMenu()
        {
            Console.WriteLine("Работа с файлом данных");
            Console.WriteLine("1. Создание файла");
            Console.WriteLine("2. Просмотр существующих файлов");
            Console.WriteLine("3. Загрузка данных из существующего файла");
            Console.WriteLine("4. Удаление файла");
            Console.WriteLine("5. Назад");
            Console.WriteLine();
        }

        public void PrintBooksManagementMenu()
        {
            Console.WriteLine("Управление базой данных");
            Console.WriteLine("1. Добавление новой книги в базу");
            Console.WriteLine("2. Редактирование информации о книге");
            Console.WriteLine("3. Удаление книги из базы");
            Console.WriteLine("4. Назад");
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
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }

        public void FileManagement()
        {
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

                        if (!result.IsSuccess)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine(error);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Файл успешно создан");
                        }

                        Console.WriteLine("** Нажмите любую клавишу для выхода **");
                        Console.ReadKey();

                        break;
                    }

                    case ConsoleKey.D2:
                    {
                        Console.Clear();
                        Console.WriteLine("Просмотр существующих файлов");

                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId}. {fileName}");
                            }
                        }

                        Console.WriteLine("** Нажмите любую клавишу для выхода **");
                        Console.ReadKey();

                        break;
                    }

                    case ConsoleKey.D3:
                    {
                        Console.Clear();
                        Console.WriteLine("Загрузка данных из существующего файла");
                        Console.WriteLine("Выберите номер файла");
                        Console.WriteLine($"Для выхода нажмите {Constants.OperationConstants.ReturnOperationId}");
                        Console.WriteLine("!!! Внимание, данное действие сотрет все несохраненные данные");

                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId}. {fileName}");
                            }

                            var fileSelection = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, fileInfos.Keys.Max());

                            if (fileSelection == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            var result = FileService.ReadFile(fileInfos[fileSelection]);

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

                        Console.WriteLine("** Нажмите любую клавишу для выхода **");
                        Console.ReadKey();
                        break;
                    }

                    case ConsoleKey.D4:
                    {
                        Console.Clear();
                        Console.WriteLine("Удаление файла");
                        Console.WriteLine("Выберите номер файла");
                        Console.WriteLine($"Для выхода нажмите {Constants.OperationConstants.ReturnOperationId}");

                        var fileInfos = FileService.GetCurrentFiles();

                        if (fileInfos == null || fileInfos.Count == 0)
                        {
                            Console.WriteLine("На текущий момент нет файлов с данными");
                        }
                        else
                        {
                            foreach (var (fileId, fileName) in fileInfos)
                            {
                                Console.WriteLine($"{fileId}. {fileName}");
                            }

                            var fileSelection = ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId, fileInfos.Keys.Max());

                            if (fileSelection == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            var result = FileService.DeleteFile(fileInfos[fileSelection]);

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

                        Console.WriteLine("** Нажмите любую клавишу для выхода **");
                        Console.ReadKey();
                        break;
                    }
                    case ConsoleKey.D5:
                    {
                        exitToken = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("** Некорректный ввод **");
                        break;
                    }
                }
            }
        }

        public void BooksManagement()
        {
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
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();

                            var bookItem = CreateOrUpdateBook();
                            bookItem.Id = selectedBookId;

                            BusinessService.UpdateBook(bookItem);

                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление книги из базы данных");
                            Console.WriteLine("Выберите книгу:");

                            var selectedBookId = SelectBookFromList();

                            BusinessService.DeleteBook(selectedBookId);
                            break;
                        }
                    case ConsoleKey.D4:
                    {
                        exitToken = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("** Некорректный ввод **");
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
            var exitToken = true;
            while (exitToken)
            {
                Console.Clear();
                PrintUserManagementMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();

                            var users = BusinessService.GetUserListWithBooks();
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

                            BusinessService.CreateUser(userItem);
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

                            BusinessService.UpdateUser(userItem);

                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            Console.WriteLine("Удаление учетной записи");
                            Console.WriteLine("Выберите учетную запись:");

                            var selectedUserId = SelectUserFromList();

                            BusinessService.DeleteUser(selectedUserId);
                            break;
                        }
                    case ConsoleKey.D5:
                    {
                        exitToken = false;
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("** Некорректный ввод **");
                        break;
                    }
                }
            }
        }

        private int SelectUserFromList()
        {
            var users = BusinessService.GetUsers();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id}. {user.Login}");
            }

            var selectedUserId = ConsoleExtensions.ReadInteger(users.Min(x => x.Id), users.Max(x => x.Id));

            return selectedUserId;
        }

        private int SelectAuthorFromList()
        {
            var authors = BusinessService.GetAuthors();

            foreach (var author in authors)
            {
                Console.WriteLine($"{author.Id}. {author.DisplayName}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(authors.Min(x => x.Id), authors.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedAuthorId;
        }

        private int SelectPublisherFromList()
        {
            var publishers = BusinessService.GetPublishers();

            foreach (var publisher in publishers)
            {
                Console.WriteLine($"{publisher.Id}. {publisher.Name}");
            }

            var selectedAuthorId = ConsoleExtensions.ReadInteger(publishers.Min(x => x.Id), publishers.Max(x => x.Id), Constants.OperationConstants.ReturnOperationId);

            return selectedAuthorId;
        }

        private int SelectBookFromList()
        {
            var books = BusinessService.GetBooks();

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
                    if (BusinessService.IsClientCardExist(input, userId) == false)
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
