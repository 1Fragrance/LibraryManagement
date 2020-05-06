using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Data;
using LibraryManagement.View.Modules;
using System;
using System.IO;

namespace LibraryManagement.View
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(Program).Assembly.Location));

            try
            {
                using (var context = new DatabaseContext())
                {
                    using (var dataSource = new DbDataSource(context))
                    {
                        RunProgram(dataSource);
                    }
                }
            }
            catch (Exception ex)
            {
                LogToFile(ex.Message);
            }
        }

        private static void LogToFile(string str)
        {
            using (var sw = new StreamWriter(Constants.LoggingConstants.FileName, true))
            {
                sw.WriteLine(str);
            }
        }

        private static void RunProgram(DbDataSource dataSource)
        {
            var authModule = new AuthModule(dataSource);
            
            var currentUserType = authModule.SignIn();

            switch (currentUserType)
            {
                case RoleType.Client:
                {
                    var clientModule = new ClientModule(dataSource);

                    break;
                }
                case RoleType.Admin:
                {
                    var adminModule = new AdminModule(dataSource);
                    adminModule.WorkAsAdmin();

                    break;
                }
                default:
                {
                    throw new InvalidDataException();
                }
            }
        }
    }
}
