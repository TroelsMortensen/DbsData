using System;
using System.Collections.Generic;
using System.Linq;
using Goodreads.Models;

namespace Goodreads.Conversion
{
    public class Converter
    {

        public DataBaseModelContainer ConvertFromCsvModelToDbModel(List<GoodreadsItem> items)
        {
            DataBaseModelContainer container = new();
            HashSet<string> bindings = new();
            HashSet<string> shelves = new();
            HashSet<string> publishers = new();
            foreach (GoodreadsItem item in items)
            {
                if (!String.IsNullOrEmpty(item.Binding))
                    bindings.Add(item.Binding);
                if (!String.IsNullOrEmpty(item.ShelfName))
                    shelves.Add(item.ShelfName);
                string itemPubName = item.PubName;
                if (!String.IsNullOrEmpty(itemPubName))
                    publishers.Add(itemPubName);
            }

            Console.WriteLine("Done collecting bindings, shelves, publishers");
            container.Bindings = GetContainerBindings(bindings);
            Console.WriteLine("Bindings added");
            // container.Shelves = GetContainerShelves(shelves);
            container.Publishers = ContainerPublishers(publishers);
            Console.WriteLine("Publishers added");
            List<Author> authors = ConvertAuthors(items);
            AddCoAuthors(authors, items);
            container.Authors = authors;
            Console.WriteLine("Authors added");
            Console.WriteLine("Converting books..");
            container.Books = ConvertBooks(items, authors, container);
            Console.WriteLine("Books converted");
            
            return container;
        }

        private static List<Publisher> ContainerPublishers(HashSet<string> publishers)
        {
            int id = 0;
            List<Publisher> list = new();
            foreach (string publisher in publishers)
            {
                id++;
                list.Add(new Publisher
                {
                    Id = id,
                    Name = publisher
                });
            }
            return list;
        }

        private static List<Shelf> GetContainerShelves(HashSet<string> shelves)
        {
            List<Shelf> list = new();
            int id = 0;
            foreach (string shelf in shelves)
            {
                id++;
                list.Add(new Shelf
                {
                    Id = id,
                    Name = shelf
                });
            }

            return list;
        }

        private static List<Binding> GetContainerBindings(HashSet<string> bindings)
        {
            List<Binding> list = new();
            int id = 0;
            foreach (string binding in bindings)
            {
                id++;
                list.Add(new Binding
                {
                    Id = id,
                    Type = binding
                });
            }

            return list;
        }


        private List<Book> ConvertBooks(List<GoodreadsItem> items, List<Author> authors, DataBaseModelContainer container)
        {
            List<Book> books = new();
            foreach (GoodreadsItem item in items)
            {
                int? bindingId = container.Bindings.FirstOrDefault(b => b.Type.Equals(item.Binding))?.Id ;
                int? publisherId = container.Publishers.FirstOrDefault(p => p.Name.Equals(item.PubName))?.Id;
                Book b = new Book
                {
                    Title = item.Title, //.Replace("'","''"),
                    // AvgRating = item.AvgRating,
                    BookId = item.BookId,
                    // DateRead = item.DateRead,
                    // MyRating = item.MyRating == 0 ? null : item.MyRating,
                    PageCount = item.PageCount,
                    YearPublished = item.YearPublished,
                    ISBN = item.ISBN,
                    BindingId = bindingId,
                    PublisherId = publisherId, //.Replace("'","''"),
                    // Shelf = item.ShelfName,
                    // AuthorFN = first.Replace("'","''"),
                    // AuthorLN = last.Replace("'","''")
                };

                string first = item.AuthorName.Trim().Split(' ')[0].Trim();
                string last = item.AuthorName.Trim().Split(' ')[^1].Trim();
                Author? find = authors.Find(author => author.FirstName.Equals(first) && author.LastName.Equals(last));
                if (find == null)
                {
                    int stopher = 0;
                }

                b.AuthorID = find.ID;

                b.CoAuthors = FindCoAuthors(authors, b, item);
                books.Add(b);
            }

            return books;
        }

        private List<int> FindCoAuthors(List<Author> authors, Book book, GoodreadsItem goodreadsItem)
        {
            List<int> ids = new();
            foreach (string authorName in goodreadsItem.CoAuthorNames)
            {
                string first = authorName.Trim().Split(' ')[0].Trim();
                string last = authorName.Trim().Split(' ')[^1].Trim();
                Author? find = authors.Find(author => author.FirstName.Equals(first) && author.LastName.Equals(last));
                if (find == null)
                {
                    int stopher = 0;
                }

                ids.Add(find.ID);
            }

            return ids;
        }

        private void AddCoAuthors(List<Author> authors, List<GoodreadsItem> items)
        {
            foreach (GoodreadsItem item in items)
            {
                foreach (string name in item.CoAuthorNames)
                {
                    if (String.IsNullOrEmpty(name))
                        continue;
                    CreateSingleAuthor(name.Trim().Split(' '), authors);
                }
            }
        }

        private static List<Author> ConvertAuthors(List<GoodreadsItem> items)
        {
            List<Author> authors = new();
            foreach (GoodreadsItem item in items)
            {
                var strings = item.AuthorName.Split(" ");
                CreateSingleAuthor(strings, authors);
            }

            return authors;
        }

        private static void CreateSingleAuthor(string[] strings, List<Author> authors)
        {
            Author author = new();
            author.FirstName = strings[0]; //.Replace("'", "''");
            author.LastName = strings[^1]; //.Replace("'", "''");
            if (strings.Length > 2)
            {
                string middleName = "";
                for (int i = 1; i < strings.Length - 1; i++)
                {
                    middleName += strings[i];
                }

                if (!String.IsNullOrEmpty(middleName))
                {
                    author.MiddelNames = middleName;
                }
            }

            if (!authors.Any(a => a.FirstName.Equals(strings[0]) && a.LastName.Equals(strings[^1])))
            {
                author.ID = authors.Count;
                authors.Add(author);
            }
        }
    }
}