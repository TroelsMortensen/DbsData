namespace Goodreads.Models
{
    public class Book
    {
        public string BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public int MyRating { get; set; }

        public decimal AvgRating { get; set; }

        public string Binding { get; set; }

        public int PageCount { get; set; }

        public int YearPublished { get; set; }

        public string DateRead { get; set; }
    }
}