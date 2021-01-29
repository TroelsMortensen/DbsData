using System.Collections.Generic;
using System.IO;
using TodoAndUser.Models;

namespace TodoAndUser.Export
{
    public class SqlExport
    {

        private const string path =
            @"C:\Users\trmo\OneDrive - ViaUC\Courses\DBS\Session 6 - DQL - Data Query Language (SQL)\JsonPlaceHolderFiles\";
        public void Export(ModelContainer mc)
        {
            ExportCompany(mc.Companies);
            ExportUsers(mc.Users);
            ExportTodos(mc.Todos);
            ExportAlbum(mc.Albums);
            ExportPhotos(mc.Photos);
            ExportPosts(mc.Posts);
            ExportComments(mc.Comments);
        }

        private void ExportComments(List<Comment> comments)
        {
            using StreamWriter file = new StreamWriter(path + "8_comments_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var t in comments)
            {
                file.WriteLine($"INSERT INTO comment(id, post_id, name, email, body) VALUES" +
                               $" ({t.id}, {t.postId}, '{t.name}', '{t.email}', '{t.body}');");
            }
        }

        private void ExportPosts(List<Post> posts)
        {
            using StreamWriter file = new StreamWriter(path + "7_posts_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var t in posts)
            {
                file.WriteLine($"INSERT INTO post(id, account_id, title, body) VALUES" +
                               $" ({t.id}, {t.userId}, '{t.title}', '{t.body}');");
            }
        }

        private void ExportPhotos(List<Photo> photos)
        {
            using StreamWriter file = new StreamWriter(path + "6_photos_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var t in photos)
            {
                file.WriteLine($"INSERT INTO photo(id, album_id, title, url, thumbnail_url) VALUES" +
                               $" ({t.id}, {t.albumId}, '{t.title}', '{t.url}', '{t.thumbnailUrl}');");
            }
        }

        private void ExportAlbum(List<Album> albums)
        {
            using StreamWriter file = new StreamWriter(path + "5_albums_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var t in albums)
            {
                file.WriteLine($"INSERT INTO album(id, account_id, title) VALUES" +
                               $" ({t.id}, {t.userId}, '{t.title}');");
            }
        }

        private void ExportTodos(List<Todo> todos)
        {
            using StreamWriter file = new StreamWriter(path + "4_todos_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var t in todos)
            {
                file.WriteLine($"INSERT INTO todo(id, account_id, title, completed) VALUES" +
                               $" ({t.id}, {t.userId}, '{t.title}', {t.completed});");
            }
        }

        private void ExportUsers(List<User> users)
        {
            using StreamWriter file = new StreamWriter(path + "3_accounts_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (var user in users)
            {
                file.WriteLine($"INSERT INTO account(id, name, username, email, street, city, zipcode, phone, company_name) VALUES" +
                               $" ({user.id}, '{user.name}', '{user.username}', '{user.email}', '{user.address.street}', " +
                               $"'{user.address.city}', '{user.address.zipcode}', '{user.phone}', '{user.company.name}');");
            }
        }

        private void ExportCompany(List<Company> companies)
        {
            using StreamWriter file = new StreamWriter(path + "2_companies_import.sql");
            file.WriteLine("SET SCHEMA 'jsonplaceholder';");
            foreach (Company company in companies)
            {
                file.WriteLine($"INSERT INTO company(name, catch_phrase, bs) VALUES" +
                               $" ('{company.name}', '{company.catchPhrase}', '{company.bs}');");
            }
        }
    }
}