namespace Goodreads.Models
{
    public class GoodreadsItem
    {
        // shelf
        public string ShelfName { get; set; }
        
        // book
        public string BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public int MyRating { get; set; }

        public decimal AvgRating { get; set; }

        public string Binding { get; set; }

        public int PageCount { get; set; }

        public int YearPublished { get; set; }

        public string DateRead { get; set; }
        
        // author
        public string FirstName { get; set; }
        public string MiddelNames { get; set; }
        public string LastName { get; set; }
        
        // publisher
        public string PubName { get; set; }
    }
}