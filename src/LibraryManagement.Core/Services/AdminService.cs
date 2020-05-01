using LibraryManagement.Common;
using LibraryManagement.Common.Items;
using LibraryManagement.Core.Mappers;
using LibraryManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Core.Services
{
    public class AdminService : ServiceBase
    {
        private BookMapper BookMapper { get; set; }
        private UserMapper UserMapper { get; set; }

        public AdminService(DbDataSource context) : base(context)
        {
            BookMapper = new BookMapper();
            UserMapper = new UserMapper();
        }

        public void DeleteBook(int id)
        {
            Context.Books.Delete(id);
        }

        public IList<BookItem> GetBooksWithEverything()
        {
            var books = Context.Books.GetBooksIncludeAll();

            var bookItemList = books.Select(w => BookMapper.MapToItem(w)).ToList();

            return bookItemList;
        }

        public IList<BookItem> GetBooks()
        {
            var books = Context.Books.GetList();

            var bookItemList = books.Select(w => BookMapper.MapToItem(w)).ToList();

            return bookItemList;
        }

        public void CreateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            var user = UserMapper.MapToEntity(userItem);

            Context.Users.Save(user);
        }

        public void UpdateUser(UserItem userItem)
        {
            if (string.IsNullOrEmpty(userItem.LibraryCardNumber))
            {
                userItem.LibraryCardNumber = GenerateRandomClientCardNumber();
            }

            var user = Context.Users.GetEntity(userItem.Id.GetValueOrDefault());

            UserMapper.MapToEntity(userItem, user);

            Context.Users.Save(user);
        }
        
        public void DeleteUser(int id)
        {
            Context.Users.Delete(id);
        }

        public IList<UserItem> GetUsers()
        {
            var users = Context.Users.GetList();

            var userItemList = users.Select(x => UserMapper.MapToItem(x)).ToList();
            
            return userItemList;
        }


        public IList<UserItem> GetUserListWithBooks()
        {
            var users = Context.Users.GetUsersIncludeLastTakenBooks();

            var userItemList = users.Select(w => UserMapper.MapToItem(w)).ToList();

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
