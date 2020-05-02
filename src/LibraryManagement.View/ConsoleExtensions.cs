using LibraryManagement.Common.Enums;
using System;
using LibraryManagement.Common;

namespace LibraryManagement.View
{
    public static class ConsoleExtensions
    {
        public static int ReadInteger(int? lowerBound = null, int? upperBound = null, int? exceptedValue = null)
        {
            while (true)
            {
                var inputStr = Console.ReadLine();

                var parseResult = int.TryParse(inputStr, out var parsedInt);
                if (!parseResult || (lowerBound != null && parsedInt < lowerBound) || (upperBound != null && parsedInt > upperBound))
                {
                    if (exceptedValue != null && exceptedValue == parsedInt)
                    {
                        return parsedInt;
                    }

                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return parsedInt;
            }
        }

        public static RoleType ReadRoleType()
        {
            var parsedInt = ReadInteger((int) RoleType.Client, (int) RoleType.Admin);
                
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
    }
}
