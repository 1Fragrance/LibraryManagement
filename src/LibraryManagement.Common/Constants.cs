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
        }

        public static class LoggingConstants
        {
            public const string FileName = "log.txt";
        }
    }
}
