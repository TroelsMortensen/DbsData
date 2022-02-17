using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace Goodreads.Models
{
    public class Book
    {
        public string BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        // public int? MyRating { get; set; }

        // public decimal AvgRating { get; set; }

        public int? PageCount { get; set; }

        public int? YearPublished { get; set; }


        // FKs
        // public string AuthorFN { get; set; }
        // public string AuthorLN { get; set; }

        public int AuthorID { get; set; }

        public int? BindingId { get; set; }

        // public string Shelf { get; set; }
        public int? PublisherId { get; set; }
        public List<int> CoAuthors { get; set; }
        public HashSet<int> GenreIds { get; set; } = new();
    }
}