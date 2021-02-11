using System.Collections.Generic;

namespace Goodreads.Models
{
    public class DataBaseModelContainer
    {
        public List<string> Shelves { get; set; }
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<string> Publishers { get; set; }
        public List<string> Bindings { get; set; }
    }
}