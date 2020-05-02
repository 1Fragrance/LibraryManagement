using LibraryManagement.Common;
using LibraryManagement.Common.Filters;
using LibraryManagement.Common.Items;
using LibraryManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Core.Services
{
    public class AdminService : ServiceBase
    {
        private Mapper Mapper { get;  }


        public AdminService(DbDataSource context) : base(context)
        {
            Mapper = new Mapper();
        }

        public IList<BookItem> GetFilteredBooks(BookFilter filter)
        {
            var books = Context.Books.GetFilteredBooks(filter);

            var bookItems = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItems;
        }

        public void UpdateBook(BookItem bookItem)
        {
            var book = Context.Books.GetEntity(bookItem.Id.GetValueOrDefault());

            Mapper.BookMapper.MapToEntity(bookItem, book);

            Context.Books.Save(book);
        }

        public void CreateBook(BookItem bookItem)
        {
            var book = Mapper.BookMapper.MapToEntity(bookItem);

            if (book.Publisher?.Id == Constants.DataAnnotationConstants.NewEntityId)
            {
                Context.Publishers.Save(book.Publisher);
            }

            if (book.Author?.Id == Constants.DataAnnotationConstants.NewEntityId)
            {
                Context.Authors.Save(book.Author);
            }

            Context.Books.Save(book);
        }

        public IList<AuthorItem> GetAuthors()
        {
            var authors = Context.Authors.GetList();

            var authorItemList = authors.Select(w => Mapper.AuthorMapper.MapToItem(w)).ToList();

            return authorItemList;
        }

        public IList<PublisherItem> GetPublishers()
        {
            var publishers = Context.Publishers.GetList();

            var publishersItemList = publishers.Select(w => Mapper.PublisherMapper.MapToItem(w)).ToList();

            return publishersItemList;
        }

        public void DeleteBook(int id)
        {
            Context.Books.Delete(id);
        }

        public IList<BookItem> GetBooksWithEverything()
        {
            var books = Context.Books.GetBooksIncludeAll();

            var bookItemList = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItemList;
        }

        public IList<BookItem> GetBooks()
        {
            var books = Context.Books.GetList();

            var bookItemList = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItemList;
        }

        public void CreateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            var user = Mapper.UserMapper.MapToEntity(userItem);

            Context.Users.Save(user);
        }

        public void UpdateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            var user = Context.Users.GetEntity(userItem.Id.GetValueOrDefault());

            Mapper.UserMapper.MapToEntity(userItem, user);

            Context.Users.Save(user);
        }
        
        public void DeleteUser(int id)
        {
            Context.Users.Delete(id);
        }

        public IList<UserItem> GetUsers()
        {
            var users = Context.Users.GetList();

            var userItemList = users.Select(x => Mapper.UserMapper.MapToItem(x)).ToList();
            
            return userItemList;
        }


        public IList<UserItem> GetUserListWithBooks()
        {
            var users = Context.Users.GetUsersIncludeLastTakenBooks();

            var userItemList = users.Select(w => Mapper.UserMapper.MapToItem(w)).ToList();

            return userItemList;
        }

        public bool IsClientCardExist(string clientCardNumber, int? userId = null)
        {
            return Context.Users.IsClientCardNumberExist(clientCardNumber, userId);
        }

        private static string GenerateRandomClientCardNumber()
        {
            return Guid.NewGuid().ToString().Take(Constants.DataAnnotationConstants.LibraryCardNumberMaxLength).ToString();
        }
    }
}
