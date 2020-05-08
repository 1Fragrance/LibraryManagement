using System;
using System.IO;
using LibraryManagement.Common;
using LibraryManagement.Core.Services.Serialization;

namespace LibraryManagement.View.Modules.BusinessLogic.Admin
{
    public partial class AdminModule
    {
        private FileService FileService { get; }

        public void PrintFileManagementMenu()
        {
            Console.WriteLine("Работа с файлом данных");
            Console.WriteLine("1. Создание файла");
            Console.WriteLine("2. Просмотр существующих файлов");
            Console.WriteLine("3. Загрузка данных из существующего файла");
            Console.WriteLine("4. Удаление файла");
            Console.WriteLine("5. Назад");
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
                                Console.WriteLine(error.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Файл данных \"{fileName}\" успешно создан");
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
                                    Console.WriteLine(error.Message);
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

                            var fileSelection =
                                ConsoleExtensions.ReadInteger(Constants.OperationConstants.ReturnOperationId,
                                    fileInfos.Count);

                            if (fileSelection == Constants.OperationConstants.ReturnOperationId)
                            {
                                break;
                            }

                            var result = FileService.DeleteFile(fileInfos[fileSelection - 1]);

                            if (!result.IsSuccess)
                            {
                                foreach (var error in result.Errors)
                                {
                                    Console.WriteLine(error.Message);
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
    }
}