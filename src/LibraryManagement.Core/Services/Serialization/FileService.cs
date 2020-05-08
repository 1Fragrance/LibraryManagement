using System;
using LibraryManagement.Common;
using LibraryManagement.Common.Results;
using LibraryManagement.Data;
using LibraryManagement.Data.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagement.Common.Enums;

namespace LibraryManagement.Core.Services.Serialization
{
    public class FileService : ServiceBase
    {
        public FileService(DbDataSource context) : base(context)
        {
        }

        public IDictionary<int, string> GetCurrentFiles()
        {
            return Directory.GetFiles(Directory.GetCurrentDirectory())
                .Where(w => w.EndsWith(Constants.Serialization.FileExtension))
                .Select((value, index) => new {index, value})
                .ToDictionary(w => w.index, w => w.value);
        }

        public ExecutionResult DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return BadResult("Ошибка чтения. Такого файла не существует");
            }

            File.Delete(filePath);

            return SuccessResult();
        }

        public ExecutionResult ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return BadResult("Ошибка чтения. Такого файла не существует");
            }

            var strings = File.ReadAllLines(filePath).ToList();

            int index = default;
            try
            {
                Context.BeginTransaction();

                Context.Books.ClearTable();
                Context.Users.ClearTable();
                Context.Publishers.ClearTable();
                Context.Authors.ClearTable();

                for (index = 0; index < strings.Count; index++)
                {
                    var str = strings[index];
                    var recordFields = str.Split(Constants.Serialization.FileDelimiter);

                    switch (recordFields[0])
                    {
                        case nameof(AuthorEntity):
                        {
                            if (recordFields.Length != Constants.DataAnnotationConstants.AuthorTableColumnsCount)
                            {
                                return FileParseBadResult($"Ошибка чтения. Некорректная запись на строке {index + 1}");
                            }

                            var authorEntity = new AuthorEntity
                            {
                                Id = Convert.ToInt32(recordFields[1]),
                                Name = recordFields[2],
                                Surname = recordFields[3],
                                Patronymic = recordFields[4]
                            };

                            Context.Authors.SaveEntityWithId(authorEntity);
                            break;
                        }
                        case nameof(PublisherEntity):
                        {
                            if (recordFields.Length != Constants.DataAnnotationConstants.PublisherTableColumnsCount)
                            {
                                return FileParseBadResult($"Ошибка чтения. Некорректная запись на строке {index + 1}");
                            }

                            var publisherEntity = new PublisherEntity
                            {
                                Id = Convert.ToInt32(recordFields[1]),
                                Name = recordFields[2]
                            };

                            Context.Publishers.SaveEntityWithId(publisherEntity);
                            break;
                        }
                        case nameof(UserEntity):
                        {
                            if (recordFields.Length != Constants.DataAnnotationConstants.UserTableColumnsCount)
                            {
                                return FileParseBadResult($"Ошибка чтения. Некорректная запись на строке {index + 1}");
                            }

                            var userEntity = new UserEntity
                            {
                                Id = Convert.ToInt32(recordFields[1]),
                                Login = recordFields[2],
                                Password = recordFields[3],
                                Role = (RoleType) Convert.ToInt32(recordFields[4]),
                                LibraryCardNumber = recordFields[5]
                            };

                            Context.Users.SaveEntityWithId(userEntity);
                            break;
                        }
                        case nameof(BookEntity):
                        {
                            if (recordFields.Length != Constants.DataAnnotationConstants.BookTableColumnsCount)
                            {
                                return FileParseBadResult($"Ошибка чтения. Некорректная запись на строке {index + 1}");
                            }

                            var bookEntity = new BookEntity
                            {
                                Id = Convert.ToInt32(recordFields[1]),
                                RegNumber = recordFields[2],
                                Name = recordFields[3],
                                NumberOfPages = Convert.ToInt32(recordFields[4]),
                                PublicationYear = Convert.ToInt32(recordFields[5]),
                                IsBookInLibrary = Convert.ToBoolean(recordFields[6]),
                                PublisherId = Convert.ToInt32(recordFields[7]),
                                AuthorId = Convert.ToInt32(recordFields[8]),
                                LastUserId = Convert.ToInt32(recordFields[9])
                            };

                            Context.Books.SaveEntityWithId(bookEntity);
                            break;
                        }
                        default:
                        {
                            return FileParseBadResult($"Ошибка чтения. Некорректная запись на строке {index + 1}");
                        }
                    }
                }
            }
            catch (Exception)
            {
                return FileParseBadResult($"Формат файла нарушен. Ошибка на строке: {index + 1}");
            }

            Context.CommitTransaction();
            return SuccessResult();
        }

        public ExecutionResult FileParseBadResult(string message)
        {
            Context.RollbackTransaction();
            return BadResult(message);
        }

        public ExecutionResult CreateFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName + Constants.Serialization.FileExtension);

            if (File.Exists(filePath))
            {
                return BadResult("Файл с таким названием уже существует");
            }

            using (var sw = new StreamWriter(filePath, true))
            {
                var authors = Context.Authors.GetList();
                var publishers = Context.Publishers.GetList();
                var users = Context.Users.GetList();
                var books = Context.Books.GetList();

                foreach (var author in authors)
                {
                    sw.WriteLine(author.GetEntityDescriptionString());
                }

                foreach (var publisher in publishers)
                {
                    sw.WriteLine(publisher.GetEntityDescriptionString());
                }

                foreach (var user in users)
                {
                    sw.WriteLine(user.GetEntityDescriptionString());
                }

                foreach (var book in books)
                {
                    sw.WriteLine(book.GetEntityDescriptionString());
                }
            }

            return SuccessResult();
        }
    }
}

