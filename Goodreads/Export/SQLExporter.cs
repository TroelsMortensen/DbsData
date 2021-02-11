using System;
using System.Collections.Generic;
using System.IO;
using Goodreads.Models;

namespace Goodreads.Export
{
    public class SQLExporter
    {
        private const string path =
            @"C:\Users\trmo\OneDrive - ViaUC\Courses\DBS\Session 6 - DQL - Data Query Language (SQL)\GoodreadsFiles\";

        public void Export(DataBaseModelContainer container)
        {
            CreateBindings(container.Bindings);
            CreateShelves(container.Shelves);
            CreatePublishers(container.Publishers);
            CreateAuthors(container.Authors);
            CreateBooks(container.Books);
            CreateCoAuthors(container.Books);
            CreateGenres(container.Books);
            CreateBookGenres(container.Books);
        }

        private void CreateBookGenres(List<Book> books)
        {
            using StreamWriter file = new StreamWriter(path + "9_Book_Genres.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                foreach (string genre in book.Genres)
                {
                    string sql = $"INSERT INTO book_genre VALUES('{genre.Replace("-", " ")}', '{book.BookId}');";
                    file.WriteLine(sql);
                }
            }
        }

        private void CreateGenres(List<Book> books)
        {
            HashSet<string> set = new();
            foreach (Book book in books)
            {
                foreach (string genre in book.Genres)
                {
                    set.Add(genre.Replace("-", " "));
                }
            }
            using StreamWriter file = new StreamWriter(path + "8_Genres.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (string genre in set)
            {
                string sql = $"INSERT INTO genre VALUES('{genre}');";
                file.WriteLine(sql);
            }
        }

        private void CreateCoAuthors(List<Book> books)
        {
            using StreamWriter file = new StreamWriter(path + "7_CoAuthors.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                if (book.CoAuthors.Count == 0) continue;
                foreach (string coAuthor in book.CoAuthors)
                {
                    string[] authorNames = coAuthor.Trim().Split(" ");
                    string sql = $"INSERT INTO co_authors VALUES('{book.BookId}', '{authorNames[0]}', '{authorNames[^1]}');";
                    file.WriteLine(sql);
                }
            }
        }

        private void CreateBooks(List<Book> books)
        {
            using StreamWriter file = new StreamWriter(path + "6_Books.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Book book in books)
            {
                string pageCount = book.PageCount != null ? book.PageCount.ToString() : "NULL";
                string year = book.YearPublished != null ? book.YearPublished.ToString() : "NULL";
                string avgRating = book.AvgRating.ToString().Replace(",", ".");
                string date = !String.IsNullOrEmpty(book.DateRead) ? $"'{book.DateRead}'" : "NULL";
                string myRating = book.MyRating == null ? "NULL" : book.MyRating.ToString();
                string isbn = String.IsNullOrEmpty(book.ISBN) ? "NULL" : $"'{book.ISBN}'";
                string bookPublisher = String.IsNullOrEmpty(book.Publisher) ? "NULL" : $"'{book.Publisher}'";
                var bookBinding = String.IsNullOrEmpty(book.Binding) ? "NULL" : $"'{book.Binding}'";
                file.WriteLine($"INSERT INTO book VALUES(" +
                               $"'{book.BookId}'," +
                               $"{isbn}," +
                               $"'{book.Title}'," +
                               $"{myRating}," +
                               $"{avgRating}," +
                               $"{pageCount}," +
                               $"{year}," +
                               $"{date}," +
                               $"{bookBinding}," +
                               $"{bookPublisher}," +
                               $"'{book.Shelf}'," +
                               $"'{book.AuthorFN}'," +
                               $"'{book.AuthorLN}'" +
                               $");");
            }
        }

        private void CreateAuthors(List<Author> authors)
        {
            using StreamWriter file = new StreamWriter(path + "5_Authors.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (Author author in authors)
            {
                file.WriteLine(
                    $"INSERT INTO author VALUES('{author.FirstName}', '{author.MiddelNames}', '{author.LastName}');");
            }
        }

        private void CreatePublishers(List<string> publishers)
        {
            using StreamWriter file = new StreamWriter(path + "4_Publishers.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (string x in publishers)
            {
                file.WriteLine($"INSERT INTO publisher VALUES('{x}');");
            }
        }

        private void CreateShelves(List<string> shelves)
        {
            using StreamWriter file = new StreamWriter(path + "3_Shelves.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (string x in shelves)
            {
                file.WriteLine($"INSERT INTO book_shelf VALUES('{x}');");
            }
        }

        private void CreateBindings(List<string> bindings)
        {
            using StreamWriter file = new StreamWriter(path + "2_Bindings.sql");
            file.WriteLine("SET SCHEMA 'goodreads';");
            foreach (string binding in bindings)
            {
                file.WriteLine($"INSERT INTO binding_type VALUES('{binding}');");
            }
        }
    }
}