using System;
using LibraryManagement.Common.Filters;
using LibraryManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Common.Enums;

namespace LibraryManagement.Data.Repositories
{
    public class BookRepository : RepositoryBase<BookEntity>
    {
        public BookRepository(DatabaseContext context) : base(context.Book, context)
        {
        }

        public bool IsBookRegNumberExist(string regNumber, int? exceptedEntityId = null)
        {
            var query = DbSet.AsQueryable();

            if (exceptedEntityId != null)
            {
                query = query.Where(r => exceptedEntityId != r.Id);
            }

            return query.Any(w => w.RegNumber == regNumber);
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
            var query = GetFiltered(DbSet.AsQueryable(), filter);

            return query
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher)
                .ToList();
        }

        public IList<BookEntity> GetOrderedBooksAfterSelectedYear(int year)
        {
            return DbSet
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher)
                .Where(r => r.PublicationYear >= year).OrderBy(w => w.Name).ToList();
        }

        public IList<BookEntity> GetSortedBooks(BookFilteringType filteringType, bool isAsc)
        {
            var query = GetSorted(DbSet.AsQueryable(), filteringType, isAsc)
                .Include(w => w.Author)
                .Include(w => w.LastUser)
                .Include(w => w.Publisher);

            return query.ToList();
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

        private IOrderedQueryable<BookEntity> GetSorted(IQueryable<BookEntity> query, BookFilteringType filteringType, bool isAsc)
        {
            switch (filteringType)
            {
                case BookFilteringType.ByName:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.Name);
                    }

                    return query.OrderByDescending(w => w.Name);
                }
                case BookFilteringType.ByRegNumber:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.RegNumber);
                    }

                    return query.OrderByDescending(w => w.RegNumber);
                }
                case BookFilteringType.ByNumberOfPages:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.NumberOfPages);
                    }

                    return query.OrderByDescending(w => w.NumberOfPages);
                }
                case BookFilteringType.ByPublicationYear:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.PublicationYear);
                    }

                    return query.OrderByDescending(w => w.PublicationYear);
                }
                case BookFilteringType.ByIsBookInLibrary:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.IsBookInLibrary);
                    }

                    return query.OrderByDescending(w => w.IsBookInLibrary);
                }
                case BookFilteringType.ByPublisherName:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.Publisher.Name);
                    }

                    return query.OrderByDescending(w => w.Publisher.Name);
                }
                case BookFilteringType.ByAuthorName:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.Author.Surname)
                            .ThenBy(w => w.Author.Name)
                            .ThenBy(w => w.Author.Patronymic);
                    }

                    return query.OrderByDescending(w => w.Author.Surname)
                        .ThenByDescending(w => w.Author.Name)
                        .ThenByDescending(w => w.Author.Patronymic);
                }
                case BookFilteringType.ByLastUserName:
                {
                    if (isAsc)
                    {
                        return query.OrderBy(w => w.LastUser.Login);
                    }

                    return query.OrderByDescending(w => w.LastUser.Login);
                }
            }

            return query.OrderByDescending(w => w.Id);
        }
    }
}

