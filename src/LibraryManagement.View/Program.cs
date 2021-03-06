﻿using LibraryManagement.Common;
using LibraryManagement.Common.Enums;
using LibraryManagement.Core.Services.Serialization;
using LibraryManagement.Data;
using LibraryManagement.View.Modules.Auth;
using LibraryManagement.View.Modules.BusinessLogic;
using System;
using System.IO;
using LibraryManagement.View.Modules.BusinessLogic.Admin;

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
                #if DEBUG
                    Console.WriteLine(ex.Message);
                #endif
                LogToFile(ex.Message);
                LogToFile(ex.StackTrace);
            }
        }

        private static void LogToFile(string str)
        {
            var logStr = $"{DateTime.UtcNow}: Error: ";

            using (var sw = new StreamWriter(Constants.LoggingConstants.FileName, true))
            {
                sw.WriteLine(logStr + str);
            }
        }

        private static void RunProgram(DbDataSource dataSource)
        {
            while (true)
            {
                var authModule = new AuthModule(dataSource);

                var authResult = authModule.SignIn();

                switch (authResult.Role)
                {
                    case RoleType.Client:
                    {
                        var clientModule = new ClientModule(authResult.CurrentUserId, dataSource);
                        clientModule.WorkAsClient();

                        break;
                    }
                    case RoleType.Admin:
                    {
                        var adminModule = new AdminModule(authResult.CurrentUserId, dataSource);
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
}
