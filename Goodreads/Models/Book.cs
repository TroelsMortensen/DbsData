using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Goodreads.Models
{
    public class Book
    {
        public string BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public int? MyRating { get; set; }

        public decimal AvgRating { get; set; }

        public int? PageCount { get; set; }

        public int? YearPublished { get; set; }

        public string DateRead { get; set; }

        // FKs
        public string AuthorFN { get; set; }
        public string AuthorLN { get; set; }
        public string Binding { get; set; }
        public string Shelf { get; set; }
        public string Publisher { get; set; }
        public List<string> CoAuthors { get; set; }
        public HashSet<string> Genres { get; set; } = new();
    }
}