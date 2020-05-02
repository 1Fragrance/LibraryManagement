using LibraryManagement.Common.Filters;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Data.Repositories
{
    public class BookRepository : RepositoryBase<BookEntity>
    {
        public BookRepository(DatabaseContext context) : base(context.Book, context)
        {
        }

        public IList<BookEntity> GetBooksIncludeAll()
        {
            return DbSet
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher)
                .ToList();
        }

        public IList<BookEntity> GetFilteredBooks(BookFilter filter)
        {
            var query =  GetFiltered(DbSet.AsQueryable(), filter);

            return query
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher)
                .ToList();
        }

        public IQueryable<BookEntity> GetFiltered(IQueryable<BookEntity> query, BookFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.Name))
            {
                var expression = GetLikeExpression(filter.Name);
                query = query.Where(w => EF.Functions.Like(w.Name, expression));
            }
            if (!string.IsNullOrEmpty(filter.RegNumber))
            {
                query = query.Where(r => filter.RegNumber == r.RegNumber);
            }
            if (filter.NumberOfPages != null)
            {
                query = query.Where(r => r.NumberOfPages == filter.NumberOfPages);
            }
            if (filter.PublicationYear != null)
            {
                query = query.Where(w => w.PublicationYear == filter.PublicationYear);
            }
            if (filter.IsBookInLibrary != null)
            {
                query = query.Where(w => w.IsBookInLibrary == filter.IsBookInLibrary);
            }
            if (!string.IsNullOrEmpty(filter.PublisherName))
            {
                var expression = GetLikeExpression(filter.PublisherName);
                query = query.Where(w => EF.Functions.Like(w.Publisher.Name, expression));
            }
            if (!string.IsNullOrEmpty(filter.AuthorName))
            {
                var expression = GetLikeExpression(filter.AuthorName);
                query = query.Where(w => EF.Functions.Like(w.Author.Surname, expression) ||
                                                 EF.Functions.Like(w.Author.Name, expression) ||
                                                 EF.Functions.Like(w.Author.Patronymic, expression));
            }
            if (!string.IsNullOrEmpty(filter.LastUserName))
            {
                var expression = GetLikeExpression(filter.LastUserName);
                query = query.Where(w => EF.Functions.Like(w.LastUser.Login, expression));
            }

            return query;
        }
    }
}
