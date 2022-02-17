namespace Goodreads.Models
{
    public class BookRead
    {
        public Profile Profile { get; set; }
        public Book Book { get; set; }
        public int? Rating { get; set; }
        public string? DateFinishedReading { get; set; }
        public string? DateStartedReading { get; set; }
        public string Status { get; set; }
    }
}