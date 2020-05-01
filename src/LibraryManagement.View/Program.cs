using System;
using LibraryManagement.Core;
using LibraryManagement.Core.Services;

namespace LibraryManagement.View
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var context = new DatabaseContext())
            {
                using (var dataSource = new DbDataSource(context))
                {
                    RunProgram(dataSource);
                }
            }
        }

        private static void RunProgram(DbDataSource dataSource)
        {
            var adminService = new AdminService(dataSource);
            var clientService = new ClientService(dataSource);
            var authService = new AuthService(dataSource);

            while (true)
            {
                Console.WriteLine("Добро пожаловать в LibraryManagement:\n" +
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
