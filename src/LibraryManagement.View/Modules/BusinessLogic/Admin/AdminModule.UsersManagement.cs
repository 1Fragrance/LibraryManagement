using System;
using System.Linq;
using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Items;

namespace LibraryManagement.View.Modules.BusinessLogic.Admin
{
    public partial class AdminModule
    {
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
                                Console.WriteLine(
                                    $"Последние взятые книги: {user.LastTakenBooks.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
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
                                Console.WriteLine(
                                    $"Книги на руках: {user.LastTakenBooks?.Select(w => w.Name).Aggregate((i, j) => i + ", " + j)}");
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

                        Console.WriteLine(
                            "Введите уникальный 6-значный номер читательского билета или оставьте пустым для автоматической генерации");
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

                        Console.WriteLine(
                            "Введите новый уникальный 6-значный номер читательского билета или оставьте пустым для автоматической генерации");
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
    }
}