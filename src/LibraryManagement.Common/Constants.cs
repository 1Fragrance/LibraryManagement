using System;

namespace LibraryManagement.Common
{
    public static class Constants
    {
        public static class DatabaseConstants
        {
            public const string ConnectionString = "Server=localhost; Initial Catalog=LibraryManagementDb; Integrated Security=True";
        }

        public static class DataAnnotationConstants
        {
            public const int StringMaxLengthValue = 255;
            public const int LibraryCardNumberMaxLength = 6;

            public const int YearMinValue = 1900;

            public const int NewEntityId = 0;
        }

        public static class LoggingConstants
        {
            public const string FileName = "log.txt";
        }

        public static class OperationConstants
        {
            public const int AddNewSubEntityOperationId = 1;
            public const int SelectSubEntityOperationId = 2;
            public const int ReturnOperationId = 0;
        }

        public static class Strings
        {
            public const string Yes = "Да";
            public const string No = "Нет";
        }
    }
}
