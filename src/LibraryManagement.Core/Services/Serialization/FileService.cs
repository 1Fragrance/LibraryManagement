using LibraryManagement.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagement.Common;
using LibraryManagement.Common.Results;
using LibraryManagement.Data.Entities;

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

                // TODO: Try shell
                switch (recordFields[0])
                {
                    // TODO: Count validation
                    case nameof(AuthorEntity):
                    {
                        break;
                    }
                    case nameof(PublisherEntity):
                    {
                        break;
                    }
                    case nameof(UserEntity):
                    {
                        break;
                    }
                    case nameof(BookEntity):
                    {
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

