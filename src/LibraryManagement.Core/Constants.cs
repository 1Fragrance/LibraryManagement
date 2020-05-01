namespace LibraryManagement.Core
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
        }

        public static class LoggingConstants
        {
            public const string FileName = "log.txt";
        }
    }
}
