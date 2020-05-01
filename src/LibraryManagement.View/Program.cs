using LibraryManagement.Core;
using LibraryManagement.Core.Enums;
using LibraryManagement.View.Modules;
using System;
using System.IO;
using System.Linq;

namespace LibraryManagement.View
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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

                    Console.WriteLine("ClientPart");
                    break;
                }
                case RoleType.Admin:
                {
                    var adminModule = new AdminModule(dataSource);
                    break;
                }
                default:
                {
                    throw new InvalidDataException();
                }
            }



                            case RoleType.Admin:
                            {

                                

                                    case ConsoleKey.D4:
                                    {
                                        Console.Clear();
                                        

                                        choice = Console.ReadKey();
                                        switch (choice.Key)
                                        {
                                            case ConsoleKey.D1:
                                            {
                                                break;
                                            }

                                            case ConsoleKey.D2:
                                            {
                                                break;
                                            }

                                            case ConsoleKey.D3:
                                            {
                                                break;
                                            }
                                        }

                                        break;
                                    }

                                    case ConsoleKey.D5:
                                    {
                                        Environment.Exit(0);
                                        break;
                                    }
                                }

                                
                                break;
                            }
                        }

                        break;
                    }
                    case ConsoleKey.D2:
                    {
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
}
    }
}
