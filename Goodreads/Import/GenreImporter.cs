using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Goodreads.Models;

namespace Goodreads.Import
{
    public class GenreImporter
    {
        public void AddGenres(List<Book> books)
        {
            using StreamReader reader = new StreamReader("BooksWithGenres.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var strings = line.Split(",");
                Book book = books.First(b => b.BookId.Equals(strings[0]));
                for (int i = 1; i < strings.Length; i++)
                {
                    book.Genres.Add(strings[i]);
                }
            }
            
        }

        private void GetGenres(Book book)
        {
            string id = book.BookId;
            
            var genres = GetFromGoodreads(id);

            book.Genres = genres;
            int stopher = 0;
        }

        private static HashSet<string> GetFromGoodreads(string id)
        {
            string urlAddress = "https://www.goodreads.com/book/show/" + id;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse) request.GetResponse();

            HashSet<string> genres = new();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                foreach (string line in data.Split("\n"))
                {
                    if (line.Contains("<a class=\"actionLinkLite bookPageGenreLink\" href=\"/genres/"))
                    {
                        string genre = line.Replace("<a class=\"actionLinkLite bookPageGenreLink\" href=\"/genres/", "")
                            .Split("\"")[0].Trim();
                        genres.Add(genre);
                    }
                }

                response.Close();
                readStream.Close();
            }

            return genres;
        }

        public void StoreBookIds(List<Book> containerBooks)
        {
            using StreamWriter file = new StreamWriter("Books.txt");
            foreach (Book book in containerBooks)
            {
                file.WriteLine(book.BookId);
            }
        }

        public void FetchOneByOne()
        {
            // first check which I have the data from
            HashSet<string> hasFetched = new();
            if (File.Exists("BooksWithGenres.txt"))
            {
                using StreamReader readerHasCompleted = new StreamReader("BooksWithGenres.txt");
                string line;
                while ((line = readerHasCompleted.ReadLine()) != null)
                {
                    hasFetched.Add(line.Split(",")[0]);
                }
            }
            
            // now try to fetch more data
            using StreamReader readerAllBooks = new StreamReader("Books.txt");
            using StreamWriter writer = new StreamWriter("BooksWithGenres.txt", true);

            try
            {
                string line1;
                while ((line1 = readerAllBooks.ReadLine()) != null)
                {
                    string id = line1;
                    if(hasFetched.Contains(id)) 
                        continue;

                    HashSet<string> genresFromGoodreads = GetFromGoodreads(id);
                    string result = id;
                    foreach (string g in genresFromGoodreads)
                    {
                        result += "," + g;
                    }
                    writer.WriteLine(result);
                    Thread.Sleep(3000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            
            
        }
    }
}