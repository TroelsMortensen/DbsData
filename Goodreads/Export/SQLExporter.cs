using System;
using System.Collections.Generic;
using System.IO;
using Goodreads.Models;

namespace Goodreads.Export
{
    public class SQLExporter
    {
        private const string path =
            @"C:\TRMO\RiderProjects\DbsData\Goodreads\";

        public void Export(DataBaseModelContainer container)
        {
            CreateBindings(container.Bindings);
            // CreateShelves(container.Shelves);
            CreatePublishers(container.Publishers);
            CreateAuthors(container.Authors);
            CreateBooks(container.Books);
            CreateCoAuthors(container.Books);
            CreateGenres(container);
            CreateBookGenres(container.Books);
            CreateUsers(container.Users);
            CreateBooksRead(container.UsersHaveRead);
        }

        private void CreateBooksRead(List<BookRead> list)
        {
            Console.WriteLine("Exporting books read.." );
            using StreamWriter file = new(path + "011_Books_Read.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (BookRead bookRead in list)
            {
                int? rating = bookRead.Rating;
                string dateStarted = bookRead.DateStartedReading ?? "NULL";
                string dateFinished = bookRead.DateFinishedReading ?? "NULL";
                string sql = $"INSERT INTO book_read VALUES({bookRead.Profile.Id},{bookRead.Book.BookId}, {rating}, '{dateStarted}', '{dateFinished}', '{bookRead.Status}');";
                file.WriteLine(sql);
            }

            Console.WriteLine("Done");
        }

        private void CreateUsers(List<Profile> users)
        {
            Console.WriteLine("Exporting users..");
            using StreamWriter file = new(path + "010_Users.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Profile user in users)
            {
                string sql = $"INSERT INTO profile VALUES({user.Id}, '{user.FirstName}', '{user.LastName}', '{user.ProfileName}');";
                file.WriteLine(sql);
            }

            Console.WriteLine("Done");
        }

        private void CreateBookGenres(List<Book> books)
        {
            Console.WriteLine("Exporting book genres..");
            using StreamWriter file = new StreamWriter(path + "009_Book_Genres.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                foreach (int genreId in book.GenreIds)
                {
                    string sql = $"INSERT INTO book_genre VALUES('{genreId}', '{book.BookId}');";
                    file.WriteLine(sql);
                }
            }

            Console.WriteLine("Done");
        }

        private void CreateGenres(DataBaseModelContainer container)
        {
            Console.WriteLine("Exporting genres");   
            using StreamWriter file = new StreamWriter(path + "008_Genres.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (GenreContainer genre in container.Genres)
            {
                string sql = $"INSERT INTO genre VALUES({genre.Id}, '{genre.Genre}');";
                file.WriteLine(sql);
            }

            Console.WriteLine("Done");
        }

        private void CreateCoAuthors(List<Book> books)
        {
            Console.WriteLine("Exporting co-authors..");
            using StreamWriter file = new StreamWriter(path + "007_CoAuthors.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                if (book.CoAuthors.Count == 0) continue;
                foreach (int coAuthor in book.CoAuthors)
                {
                    string sql = $"INSERT INTO co_authors VALUES('{book.BookId}', {coAuthor});";
                    file.WriteLine(sql);
                }
            }

            Console.WriteLine("Done");
        }

        private void CreateBooks(List<Book> books)
        {
            Console.WriteLine("Exporting books..");
            using StreamWriter file = new StreamWriter(path + "006_Books.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                string pageCount = book.PageCount != null ? book.PageCount.ToString() : "NULL";
                string year = book.YearPublished != null ? book.YearPublished.ToString() : "NULL";
                string isbn = String.IsNullOrEmpty(book.ISBN) ? "NULL" : $"'{book.ISBN}'";
                string bookPublisherId = book.PublisherId == null ? "NULL" : book.PublisherId.ToString();
                string bookBindingId = book.BindingId == null ? "NULL" : book.BindingId.ToString();
                var bookTitle = book.Title.Replace("'","''");
                file.WriteLine($"INSERT INTO book VALUES(" +
                               $"'{book.BookId}'," +
                               $"{isbn}," +
                               $"'{bookTitle}'," +
                               $"{pageCount}," +
                               $"{year}," +
                               $"{bookBindingId}," +
                               $"{bookPublisherId}," +
                               $"{book.AuthorID}" +
                               $");");
            }

            Console.WriteLine("Done");
        }

        private void CreateAuthors(List<Author> authors)
        {
            Console.WriteLine("Exporting authors..");
            using StreamWriter file = new StreamWriter(path + "005_Authors.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Author author in authors)
            {
                string middleName = author.MiddelNames?.Replace("'","''");
                middleName = String.IsNullOrEmpty(middleName) ? "NULL" : "'" + middleName + "'";
                file.WriteLine(
                    $"INSERT INTO author VALUES({author.ID},'{author.FirstName.Replace("'","''")}', {middleName}, '{author.LastName.Replace("'","''")}');");
            }

            Console.WriteLine("Done");
        }

        private void CreatePublishers(List<Publisher> publishers)
        {
            Console.WriteLine("Exporting publishers..");
            using StreamWriter file = new StreamWriter(path + "004_Publishers.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Publisher pub in publishers)
            {
                file.WriteLine($"INSERT INTO publisher VALUES({pub.Id},'{pub.Name.Replace("'","''")}');");
            }

            Console.WriteLine("Done");
        }

        private void CreateShelves(List<string> shelves)
        {
            using StreamWriter file = new StreamWriter(path + "003_Shelves.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (string x in shelves)
            {
                file.WriteLine($"INSERT INTO book_shelf VALUES('{x}');");
            }
        }

        private void CreateBindings(List<Binding> bindings)
        {
            Console.WriteLine("Exporting bindings..");
            using StreamWriter file = new StreamWriter(path + "002_Bindings.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Binding binding in bindings)
            {
                file.WriteLine($"INSERT INTO binding_type VALUES({binding.Id}, '{binding.Type}');");
            }

            Console.WriteLine("Done");
        }
    }
}