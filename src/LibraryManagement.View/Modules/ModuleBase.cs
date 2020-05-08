using System;

namespace LibraryManagement.View.Modules
{
    public abstract class ModuleBase
    {
        protected readonly int? CurrentUserId;

        protected ModuleBase(int? currentUserId)
        {
            this.CurrentUserId = currentUserId;
        }

        protected ModuleBase() : this(null)
        {
        }

        public abstract void PrintMainMenu();

        protected static void PrintIncorrectInput()
        {
            Console.WriteLine();
            Console.WriteLine("** Некорректный ввод! **");
            Console.WriteLine();
        }

        protected static void PrintPressAnyBottom()
        {
            Console.WriteLine();
            Console.WriteLine("** Нажмите любую клавишу для выхода **");
            Console.ReadKey();
        }
    }
}
