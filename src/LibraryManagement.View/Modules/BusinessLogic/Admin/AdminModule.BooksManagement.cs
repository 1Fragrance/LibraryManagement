using System;
using LibraryManagement.Common;
using LibraryManagement.Common.Items;

namespace LibraryManagement.View.Modules.BusinessLogic.Admin
{
    public partial class AdminModule
    {
        public void PrintBooksManagementMenu()
        {
            Console.WriteLine("Управление базой данных");
            Console.WriteLine("1. Добавление новой книги в базу");
            Console.WriteLine("2. Редактирование информации о книге");
            Console.WriteLine("3. Удаление книги из базы");
            Console.WriteLine("4. Назад");
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
            var authorOperationChoice = ConsoleExtensions.ReadInteger(
                Constants.OperationConstants.AddNewSubEntityOperationId,
                Constants.OperationConstants.SelectSubEntityOperationId);

            var authorItem = new AuthorItem();
            switch (authorOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие авторы:");
                    Console.WriteLine(
                        $"Нажмите \"{Constants.OperationConstants.ReturnOperationId}\" для добавления нового автора");
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
            var publisherOperationChoice = ConsoleExtensions.ReadInteger(
                Constants.OperationConstants.AddNewSubEntityOperationId,
                Constants.OperationConstants.SelectSubEntityOperationId);

            var publisherItem = new PublisherItem();
            switch (publisherOperationChoice)
            {
                case Constants.OperationConstants.SelectSubEntityOperationId:
                {
                    Console.WriteLine("Существующие издатели:");
                    Console.WriteLine(
                        $"Нажмите \"{Constants.OperationConstants.ReturnOperationId}\" для добавления нового издателя");
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
    }
}