using System.Collections.Generic;

namespace TodoAndUser.Models
{
    public class ModelContainer
    {
        public List<Album> Albums { get; set; } = new List<Album>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Company> Companies { get; set; } = new List<Company>();
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public List<User> Users { get; set; } = new List<User>();
    }
}