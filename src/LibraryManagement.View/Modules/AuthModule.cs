using LibraryManagement.Common.Enums;
using LibraryManagement.Data;
using System;
using LibraryManagement.Core.Services.Auth;

namespace LibraryManagement.View.Modules
{
    public class AuthModule : ModuleBase
    {
        private AuthService AuthService { get; }

        public AuthModule(DbDataSource dataSource)
        {
            AuthService = new AuthService(dataSource);    
        }

        private static void PrintWelcomeMessage()
        {
            Console.WriteLine("Добро пожаловать в LibraryManagement\n" +
                              "Для того, чтобы использовать программу Вам необходимо авторизоваться");
            Console.WriteLine("1. Войти в систему");
            Console.WriteLine("2. Выйти из системы");
        }

        public RoleType SignIn()
        {
            Console.Clear();

            while (true)
            {
                PrintWelcomeMessage();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();
                        Console.WriteLine("Форма входа в аккаунт");
                        Console.WriteLine("Введите логин:");
                        var login = Console.ReadLine();
                        Console.WriteLine("Введите пароль:");
                        var password = Console.ReadLine();

                        var authResult = AuthService.SignIn(login, password);

                        if (!authResult.IsSuccess || authResult.Role == null)
                        {
                            Console.WriteLine("Такого аккаунта нет в системе\n");
                            break;
                        }

                        return authResult.Role.Value;
                    }
                    case ConsoleKey.D2:
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("** Некорректный ввод! **\n");
                        break;
                    }
                }
            }
        }
    }
}
