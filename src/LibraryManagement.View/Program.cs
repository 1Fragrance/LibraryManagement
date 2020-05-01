using LibraryManagement.Core;
using LibraryManagement.Core.Enums;
using LibraryManagement.Core.Services;
using System;
using System.IO;
using System.Linq;

namespace LibraryManagement.View
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                using (var context = new DatabaseContext())
                {
                    using (var dataSource = new DbDataSource(context))
                    {
                        RunProgram(dataSource);
                    }
                }
            }
            catch (Exception ex)
            {
                LogToFile(ex.Message);
            }
        }

        private static void LogToFile(string str)
        {
            using (var sw = new StreamWriter(Constants.LoggingConstants.FileName, true))
            {
                sw.WriteLine(str);
            }
        }

        private static void RunProgram(DbDataSource dataSource)
        {
            var adminService = new AdminService(dataSource);
            var clientService = new ClientService(dataSource);
            var authService = new AuthService(dataSource);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в LibraryManagement\n" +
                                  "Для того, чтобы использовать программу Вам необходимо авторизоваться");
                Console.WriteLine("1. Войти в систему");
                Console.WriteLine("2. Выйти из системы");

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();
                        Console.WriteLine("Введите логин:");
                        var login = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        var password = Console.ReadLine();

                        var authResult = authService.SignIn(login, password);

                        if (!authResult.IsSuccess)
                        {
                            break;
                        }

                        switch (authResult.Role)
                        {
                            case RoleType.Client:
                            {
                                Console.WriteLine("");
                                break;
                            }

                            case RoleType.Admin:
                            {
                                Console.Clear();
                                Console.WriteLine("Система администрирования библиотекой");
                                Console.WriteLine("1. Управление учетными записями пользователей");
                                Console.WriteLine("2. Редактирование данных");
                                Console.WriteLine("3. Обработка данных");
                                Console.WriteLine("4. Работа с файлом данных");
                                Console.WriteLine("5. Выход из программы");

                                choice = Console.ReadKey();
                                switch (choice.Key)
                                {
                                    case ConsoleKey.D1:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Управление учетными записями пользователей");
                                        Console.WriteLine("1. Просмотр всех зарегистрированных учетных записей");
                                        Console.WriteLine("2. Добавление новой учетной записи");
                                        Console.WriteLine("3. Редактирование учетной записи");
                                        Console.WriteLine("4. Удаление учетной записи");

                                        choice = Console.ReadKey();
                                        switch (choice.Key)
                                        {
                                            case ConsoleKey.D1:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("* Нажмите любую клавишу для выхода *");
                                                Console.WriteLine();

                                                var users = adminService.GetUserListWithBooks();
                                                foreach (var user in users)
                                                {
                                                    Console.WriteLine($"Id: {user.Id}");
                                                    Console.WriteLine($"Логин: {user.Login}");
                                                    Console.WriteLine($"Роль: {user.Role.GetDescription()}");
                                                    Console.WriteLine($"Взятые книги: {user.LastTakenBooks?.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
                                                    Console.WriteLine();
                                                }

                                                Console.ReadKey();

                                                break;
                                            }

                                            case ConsoleKey.D2:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Создание новой учетной записи");
                                                Console.WriteLine("Введите логин:");
                                                var newAccountLogin = Console.ReadLine();
                                                Console.WriteLine("Введите пароль:");
                                                var newAccountPassword = Console.ReadLine();

                                                int newAccountRoleType;
                                                while (true)
                                                {
                                                    Console.WriteLine("Тип записи: 0 - пользователь, 1 - администратор:"); 
                                                    var newAccountIsAdmin = Console.ReadLine();
                                                    
                                                    var parseResult = int.TryParse(newAccountIsAdmin, out newAccountRoleType);
                                                    if (!parseResult)
                                                    {
                                                        continue;
                                                    }

                                                    break;
                                                }

                                                adminService.CreateUser(login, password, newAccountRoleType);
                                                
                                                break;
                                            }

                                            case ConsoleKey.D3:
                                            {
                                                Console.Clear();
                                                Console.WriteLine("Редактирование существующей учетной записи");
                                                Console.WriteLine("Выберите учетную запись:");

                                                var users = adminService.GetUsers();

                                                foreach (var user in users)
                                                {
                                                    Console.WriteLine($"{user.Id}. {user.Login}");
                                                }

                                                choice = Console.ReadKey();

                                                break;
                                            }

                                            case ConsoleKey.D4:
                                            {
                                                break;
                                            }
                                        }

                                        break;
                                    }
                                    case ConsoleKey.D2:
                                    {
                                       

                                        break;
                                    }
                                    case ConsoleKey.D3:
                                    {


                                        break;
                                    }

                                    case ConsoleKey.D4:
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Работа с файлом данных");
                                        Console.WriteLine("1. Создание файла");
                                        Console.WriteLine("2. Открытие существующего файла");
                                        Console.WriteLine("3. Удаление файла");

                                        choice = Console.ReadKey();
                                        switch (choice.Key)
                                        {
                                            case ConsoleKey.D1:
                                            {
                                                break;
                                            }

                                            case ConsoleKey.D2:
                                            {
                                                break;
                                            }

                                            case ConsoleKey.D3:
                                            {
                                                break;
                                            }
                                        }

                                        break;
                                    }

                                    case ConsoleKey.D5:
                                    {
                                        Environment.Exit(0);
                                        break;
                                    }
                                }

                                
                                break;
                            }
                        }

                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
        }
    }
}
