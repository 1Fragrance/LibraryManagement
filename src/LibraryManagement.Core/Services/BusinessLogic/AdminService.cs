using LibraryManagement.Common;
using LibraryManagement.Common.Items;
using LibraryManagement.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibraryManagement.Common.Results;

namespace LibraryManagement.Core.Services.BusinessLogic
{
    public class AdminService : BusinessLogicServiceBase
    {
        public AdminService(DbDataSource context) : base(context)
        {
        }

        public ExecutionResult UpdateBook(BookItem bookItem)
        {
            if (Context.Books.IsBookRegNumberExist(bookItem.RegNumber, bookItem.Id))
            {
                return BadResult("Такой регистрационный номер уже есть в системе");
            }

            var book = Context.Books.GetEntity(bookItem.Id.GetValueOrDefault());

            Mapper.BookMapper.MapToEntity(bookItem, book);

            Context.Books.Save(book);
            return SuccessResult();
        }

        public ExecutionResult CreateBook(BookItem bookItem)
        {
            if (Context.Books.IsBookRegNumberExist(bookItem.RegNumber))
            {
                return BadResult("Такой регистрационный номер уже есть в системе");
            }

            var book = Mapper.BookMapper.MapToEntity(bookItem);

            if (book.PublisherId == Constants.DataAnnotationConstants.NewEntityId)
            {
                Context.Publishers.Save(book.Publisher);
            }

            if (book.Author?.Id == Constants.DataAnnotationConstants.NewEntityId)
            {
                Context.Authors.Save(book.Author);
            }

            Context.Books.Save(book);
            return SuccessResult();
        }

        public void DeleteBook(int id)
        {
            Context.Books.Delete(id);
        }

        public IList<BookItem> GetBooks()
        {
            var books = Context.Books.GetList();
            var bookItemList = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();
            return bookItemList;
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

        public ExecutionResult CreateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            if (Context.Users.IsLoginExist(userItem.Login))
            {
                return BadResult("Такой логин уже есть в системе");
            }

            var user = Mapper.UserMapper.MapToEntity(userItem);

            Context.Users.Save(user);
            return SuccessResult();
        }

        public ExecutionResult UpdateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            if (Context.Users.IsLoginExist(userItem.Login, userItem.Id))
            {
                return BadResult("Такой логин уже есть в системе");
            }

            var user = Context.Users.GetEntity(userItem.Id.GetValueOrDefault());

            Mapper.UserMapper.MapToEntity(userItem, user);

            Context.Users.Save(user);
            return SuccessResult();
        }
        
        public void DeleteUser(int id)
        {
            Context.Users.Delete(id);
        }

        // TODO: Move arg to filter
        public IList<UserItem> GetUsers(int? exceptedId = null)
        {
            var users = Context.Users.GetList(exceptedId);
            var userItemList = users.Select(x => Mapper.UserMapper.MapToItem(x)).ToList();
            return userItemList;
        }

        public IList<UserItem> GetUsersIncludeBooks()
        {
            var users = Context.Users.GetListIncludeBooks();
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
            return Path.GetRandomFileName().Replace(".", "").Substring(0, Constants.DataAnnotationConstants.LibraryCardNumberMaxLength);
        }
    }
}
