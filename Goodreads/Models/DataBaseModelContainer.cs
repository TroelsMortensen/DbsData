using System.Collections.Generic;

namespace Goodreads.Models
{
    public class DataBaseModelContainer
    {
        public List<Shelf> Shelves { get; set; }
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<Publisher> Publishers { get; set; }
        public List<Binding> Bindings { get; set; }
        public List<Profile> Users { get; set; }
        public List<BookRead> UsersHaveRead { get; set; }
        public List<GenreContainer> Genres { get; set; }
        
    }
}