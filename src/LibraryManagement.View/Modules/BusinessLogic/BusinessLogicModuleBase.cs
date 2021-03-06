﻿using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Filters;
using LibraryManagement.Common.Items;
using LibraryManagement.Core.Services.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.View.Modules.BusinessLogic
{
    public abstract class BusinessLogicModuleBase<T> : ModuleBase where T : BusinessLogicServiceBase
    {
        protected T BusinessService { get; set; }

        protected BusinessLogicModuleBase(int currentUserId) : base(currentUserId)
        {
        }

        public void PrintBooksViewMenu()
        {
            Console.WriteLine("Просмотр базы данных");
            Console.WriteLine("1. Просмотр всех добавленных книг");
            Console.WriteLine("2. Вывод книг с фильтрацией по заданному полю");
            Console.WriteLine("3. Вывод книг в отсортированном виде");
            Console.WriteLine("4. Вывод книг в алфавитном порядке, изданных после заданного года");
            Console.WriteLine("5. Вывод книг находящихся в текущий момент у читателей");
            Console.WriteLine("6. Назад");
        }

        private static void PrintBookFieldSelection()
        {
            Console.WriteLine("Выберите поле");
            Console.WriteLine($"{(int)BookFilteringType.ByName}. По названию книги");
            Console.WriteLine($"{(int)BookFilteringType.ByRegNumber}. По регистрационному номеру");
            Console.WriteLine($"{(int)BookFilteringType.ByNumberOfPages}. По количеству страниц");
            Console.WriteLine($"{(int)BookFilteringType.ByPublicationYear}. По году публикации");
            Console.WriteLine($"{(int)BookFilteringType.ByIsBookInLibrary}. По наличию в библиотеке");
            Console.WriteLine($"{(int)BookFilteringType.ByPublisherName}. По названию издателя");
            Console.WriteLine($"{(int)BookFilteringType.ByAuthorName}. По имени автора");
            Console.WriteLine($"{(int)BookFilteringType.ByLastUserName}. По имени последнего читателя");
        }

        public void BooksView()
        {
            Console.Clear();

            var exitToken = true;
            while (exitToken)
            {
                Console.Clear();
                PrintBooksViewMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        {
                            Console.Clear();
                            Console.WriteLine("Вывод списка книг");
                            var books = BusinessService.GetBooksWithEverything();
                            foreach (var book in books)
                            {
                                PrintBookInfo(book);
                            }

                            PrintPressAnyBottom();
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            Console.Clear();
                            Console.WriteLine("Вывод списка книг в отфильтрованном по определенному полю виде");
                            PrintBookFieldSelection();

                            var selectedFilteringType = ConsoleExtensions.ReadInteger(Enum.GetValues(typeof(BookFilteringType)).Cast<BookFilteringType>().Cast<int>().ToList());
                            Console.Clear();
                            Console.WriteLine("Введите значение для фильтрации");

                            var filter = new BookFilter();
                            switch (selectedFilteringType)
                            {
                                case (int)BookFilteringType.ByName:
                                    {
                                        filter.Name = ConsoleExtensions.ReadNotEmptyString();
                                        break;
                                    }
                                case (int)BookFilteringType.ByRegNumber:
                                    {
                                        filter.RegNumber = ConsoleExtensions.ReadNotEmptyString();
                                        break;
                                    }
                                case (int)BookFilteringType.ByNumberOfPages:
                                    {
                                        filter.NumberOfPages = ConsoleExtensions.ReadInteger();
                                        break;
                                    }
                                case (int)BookFilteringType.ByPublicationYear:
                                    {
                                        filter.PublicationYear = ConsoleExtensions.ReadInteger();
                                        break;
                                    }
                                case (int)BookFilteringType.ByIsBookInLibrary:
                                    {
                                        Console.WriteLine("Введите либо {0}, либо {1}", Constants.Strings.Yes, Constants.Strings.No);
                                        filter.IsBookInLibrary = ConsoleExtensions.ReadBoolean();
                                        break;
                                    }
                                case (int)BookFilteringType.ByPublisherName:
                                    {
                                        filter.PublisherName = ConsoleExtensions.ReadNotEmptyString();
                                        break;
                                    }
                                case (int)BookFilteringType.ByAuthorName:
                                    {
                                        filter.AuthorName = ConsoleExtensions.ReadNotEmptyString();
                                        break;
                                    }
                                case (int)BookFilteringType.ByLastUserName:
                                    {
                                        filter.LastUserName = ConsoleExtensions.ReadNotEmptyString();
                                        break;
                                    }
                            }

                            Console.Clear();
                            var books = BusinessService.GetFilteredBooks(filter);

                            foreach (var book in books)
                            {
                                PrintBookInfo(book);
                            }

                            PrintPressAnyBottom();
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            Console.Clear();
                            Console.WriteLine("Вывод списка книг в отсортированном виде");
                            PrintBookFieldSelection();

                            var selectedOrderingType = ConsoleExtensions.ReadInteger(Enum.GetValues(typeof(BookFilteringType)).Cast<BookFilteringType>().Cast<int>().ToList());
                            Console.Clear();
                            Console.WriteLine("Вывести по возрастанию? (Да, Нет)");
                            var isAsc = ConsoleExtensions.ReadBoolean();

                            var books = BusinessService.GetSortedBooks((BookFilteringType)selectedOrderingType, isAsc);

                            Console.Clear();
;                           foreach (var book in books)
                            {
                                PrintBookInfo(book);
                            }

                            PrintPressAnyBottom();
                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            Console.Clear();
                            Console.WriteLine("Вывод книг в алфавитном порядке, изданных после заданного года");
                            Console.WriteLine("Введите год:");

                            var selectedYear = ConsoleExtensions.ReadInteger();

                            var books = BusinessService.GetOrderedBooksAfterSelectedYear(selectedYear);
                            Console.Clear();
                            foreach (var book in books)
                            {
                                PrintBookInfo(book);
                            }

                            PrintPressAnyBottom();

                            break;
                        }
                    case ConsoleKey.D5:
                        {
                            Console.Clear();
                            Console.WriteLine("Вывод книг находящихся в текущий момент у читателей");

                            var books = BusinessService.GetAlreadyTakenBooks();
                            foreach (var book in books)
                            {
                                PrintBookInfo(book);
                            }

                            PrintPressAnyBottom();
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

        private static void PrintBookInfo(BookItem book)
        {
            Console.WriteLine($"Id: {book.Id}");
            Console.WriteLine($"Регистрационный номер: {book.RegNumber}");
            Console.WriteLine($"Название: {book.Name}");
            Console.WriteLine($"Автор: {book.Author?.DisplayName}");
            Console.WriteLine($"Количество страниц: {book.NumberOfPages}");
            Console.WriteLine($"Год издания: {book.PublicationYear}");
            Console.WriteLine($"Издательство: {book.Publisher?.Name}");
            Console.WriteLine($"На руках у читателя: {(book.IsBookInLibrary ? "Нет" : "Да")}");
            if (book.LastUser != null)
            {
                Console.WriteLine($"Номер читательского билета последнего читателя {book.LastUser?.LibraryCardNumber}");
            }

            Console.WriteLine();
        }
    }
}
