using LibraryManagement.Common.Enums;
using System;
using System.Collections.Generic;
using LibraryManagement.Common;

namespace LibraryManagement.View
{
    public static class ConsoleExtensions
    {
        public static int ReadInteger(IList<int> allowedValues = null, int? maxValue = null, int? minValue = null)
        {
            while (true)
            {
                var inputStr = Console.ReadLine();

                var parseResult = int.TryParse(inputStr, out var parsedInt);
                if (!parseResult || allowedValues != null && !allowedValues.Contains(parsedInt) || maxValue != null && parsedInt > maxValue || minValue != null && parsedInt < minValue)
                {
                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return parsedInt;
            }
        }

        public static string ReadNotEmptyString()
        {
            while (true)
            {
                var str = Console.ReadLine();

                if (!string.IsNullOrEmpty(str))
                {
                    return str;
                }

                Console.WriteLine("** Некорректный ввод! **");
            }
        }

        public static RoleType ReadRoleType()
        {
            var parsedInt = ReadInteger(allowedValues :new List<int> { (int)RoleType.Client, (int)RoleType.Admin });
                
            return (RoleType) parsedInt;
        }

        public static int ReadYear()
        {
            while (true)
            {
                var parsedInt = ReadInteger();

                if (parsedInt < Constants.DataAnnotationConstants.YearMinValue || parsedInt > DateTime.UtcNow.Year)
                {
                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return parsedInt;
            }
        }

        public static bool ReadBoolean()
        {
            while (true)
            {
                var str = Console.ReadLine();

                if (!string.Equals(str, Constants.Strings.Yes, StringComparison.InvariantCultureIgnoreCase) && !string.Equals(str, Constants.Strings.No, StringComparison.InvariantCultureIgnoreCase))
                {
                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return string.Equals(str, Constants.Strings.Yes);
            }
        }

        public static string InputPassword()
        {
            string pass = "";
            do
            {
                var key = Console.ReadKey(true);
                // Backspace Should Not Work
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);

            return pass;
        }
    }
}
