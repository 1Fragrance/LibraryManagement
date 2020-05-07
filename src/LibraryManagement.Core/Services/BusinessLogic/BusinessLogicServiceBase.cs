using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Common.Enums;
using LibraryManagement.Common.Filters;
using LibraryManagement.Common.Items;
using LibraryManagement.Data;

namespace LibraryManagement.Core.Services.BusinessLogic
{
    public abstract class BusinessLogicServiceBase : ServiceBase
    {
        protected Mapper Mapper { get; }

        protected BusinessLogicServiceBase(DbDataSource context) : base(context)
        {
            Mapper = new Mapper();
        }

        public IList<BookItem> GetBooksWithEverything()
        {
            var books = Context.Books.GetBooksIncludeAll();

            var bookItemList = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItemList;
        }

        public IList<BookItem> GetFilteredBooks(BookFilter filter)
        {
            var books = Context.Books.GetFilteredBooks(filter);

            var bookItems = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItems;
        }

        public IList<BookItem> GetSortedBooks(BookFilteringType filteringType, bool isAsc)
        {
            var books = Context.Books.GetSortedBooks(filteringType, isAsc);

            var bookItems = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItems;
        }

        public IList<BookItem> GetAlreadyTakenBooks()
        {
            var books = Context.Books.GetFilteredBooks(new BookFilter { IsBookInLibrary = false });

            var bookItems = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItems;
        }


        public IList<BookItem> GetOrderedBooksAfterSelectedYear(int year)
        {
            var books = Context.Books.GetOrderedBooksAfterSelectedYear(year);

            var bookItems = books.Select(w => Mapper.BookMapper.MapToItem(w)).ToList();

            return bookItems;
        }
    }
}
