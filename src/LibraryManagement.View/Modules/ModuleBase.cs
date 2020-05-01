using System;
using LibraryManagement.Core.Enums;

namespace LibraryManagement.View.Modules
{
    public class ModuleBase
    {
        public int ReadInteger()
        {
            while (true)
            {
                var inputStr = Console.ReadLine();

                var parseResult = int.TryParse(inputStr, out var parsedInt);
                if (!parseResult)
                {
                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return parsedInt;
            }
        }

        public RoleType ReadRoleType()
        {
            while (true)
            {
                var inputStr = Console.ReadLine();

                var parseResult = int.TryParse(inputStr, out var parsedInt);
                if (!parseResult || parsedInt < (int) RoleType.Client || parsedInt > (int) RoleType.Admin)
                {
                    Console.WriteLine("** Некорректный ввод! **");
                    continue;
                }

                return (RoleType) parsedInt;
            }
        }
    }
}
