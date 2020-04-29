using System;
using System.Data.SqlClient;
using LibraryManagement.Core.Repositories;

namespace LibraryManagement.Core
{
    public class DatabaseContext : IDisposable
    {
        private SqlConnection Connection { get; }

        public AuthorRepository Author { get; }
        public UserRepository User { get; }
        public PublisherRepository Publisher { get; }
        public BookRepository Book { get; }

        public DatabaseContext()
        {
            Connection = new SqlConnection(Constants.ConnectionString);
            Connection.Open();

            Author = new AuthorRepository();
            User = new UserRepository();
            Publisher = new PublisherRepository();
            Book = new BookRepository();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
