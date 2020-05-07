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
                .Select((value, index) => new {index, value})
                .ToDictionary(w => w.index, w => w.value);
        }

        public void DeleteFile(string filePath)
        {
            // TODO: File not found result
            File.Delete(filePath);
        }

        public FileParseResult ReadFile(string fileName)
        {
            var strings = File.ReadAllLines(fileName).ToList();

            for (var i = 1; i <= strings.Count; i++)
            {
                var str = strings[i];
                var recordFields = str.Split(Constants.Serialization.FileDelimiter);

                // TODO: Begin transaction
                // TODO: Try shell
                switch (recordFields[0])
                {
                    // TODO: Count validation
                    // TODO: Type validation
                    //
                    case nameof(AuthorEntity):
                    {
                        var authorEntity = new AuthorEntity
                        {
                            Id = Convert.ToInt32(recordFields[1]),
                            Name = recordFields[2],
                            Surname = recordFields[3],
                            Patronymic = recordFields[4]
                        };

                        Context.Authors.Save(authorEntity);
                        break;
                    }
                    case nameof(PublisherEntity):
                    {
                        var publisherEntity = new PublisherEntity
                        {
                            Id = Convert.ToInt32(recordFields[1]),
                            Name = recordFields[2]
                        };

                        Context.Publishers.Save(publisherEntity);
                        break;
                    }
                    case nameof(UserEntity):
                    {
                        var userEntity = new UserEntity
                        {
                            Id = Convert.ToInt32(recordFields[1]),
                            Login = recordFields[2],
                            Password = recordFields[3],
                            Role = (RoleType) Convert.ToInt32(recordFields[4]),
                            LibraryCardNumber = recordFields[5]
                        };

                        Context.Users.Save(userEntity);
                        break;
                    }
                    case nameof(BookEntity):
                    {
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

                        Context.Books.Save(bookEntity);
                        break;
                    }
                    default:
                    {
                        // TODO: Rollback transaction
                        return new FileParseResult
                        {
                            IsSuccess = false,
                            InvalidRecordLineNumber = i
                        };
                    }
                }
            }

            return new FileParseResult
            {
                IsSuccess = true
            };
        }

        public void CreateFile(string fileName)
        {
            // TODO: Add file existing checking
            using (var sw = new StreamWriter(fileName, true))
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
        }
    }
}

