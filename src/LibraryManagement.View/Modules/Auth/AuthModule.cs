using LibraryManagement.Common.Results;
using LibraryManagement.Core.Services.Auth;
using LibraryManagement.Data;
using System;

namespace LibraryManagement.View.Modules.Auth
{
    public class AuthModule : ModuleBase
    {
        private AuthService AuthService { get; }

        public AuthModule(DbDataSource dataSource)
        {
            AuthService = new AuthService(dataSource);    
        }

        public override void PrintMainMenu()
        {
            Console.WriteLine("Добро пожаловать в LibraryManagement\n" +
                              "Для того, чтобы использовать программу Вам необходимо авторизоваться");
            Console.WriteLine("1. Войти в систему");
            Console.WriteLine("2. Выйти из системы");
        }

        public AuthResult SignIn()
        {
            while (true)
            {
                PrintMainMenu();

                var choice = Console.ReadKey();
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                    {
                        Console.Clear();
                        Console.WriteLine("Форма входа в аккаунт");
                        Console.WriteLine("Введите логин:");
                        var login = ConsoleExtensions.ReadNotEmptyString();
                        Console.WriteLine("Введите пароль:");
                        var password = ConsoleExtensions.ReadNotEmptyString();

                        var authResult = AuthService.SignIn(login, password);

                        if (!authResult.IsSuccess || authResult.Role == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Такого аккаунта нет в системе");
                            Console.WriteLine();
                            break;
                        }

                        return authResult;
                    }
                    case ConsoleKey.D2:
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
        }
    }
}
