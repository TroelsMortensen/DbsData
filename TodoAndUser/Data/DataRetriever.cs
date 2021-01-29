using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TodoAndUser.Models;

namespace TodoAndUser.Data
{
    public class DataRetriever
    {
        private const string prefix = "https://jsonplaceholder.typicode.com/";

        public async Task<ModelContainer> RetrieveAll()
        {
            ModelContainer mc = new ModelContainer();

            mc.Albums = await RetrieveList<Album>("albums");

            mc.Comments = await RetrieveList<Comment>("comments");

            mc.Photos = await RetrieveList<Photo>("photos");

            mc.Posts = await RetrieveList<Post>("posts");

            mc.Todos = await RetrieveList<Todo>("todos");

            mc.Users = await RetrieveList<User>("users");

            mc.Companies = RetrieveCompanies(mc.Users);

            return mc;
        }

        private List<Company> RetrieveCompanies(List<User> users)
        {
            List<Company> companies = new List<Company>();
            foreach (User user in users)
            {
                if (!companies.Any(c => c.name.Equals(user.company.name)))
                {
                    companies.Add(user.company);
                }
            }

            return companies;
        }

        private async Task<List<T>> RetrieveList<T>(string resource)
        {
            using (HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(prefix)
            })
            {
                string asJson = await client.GetStringAsync(resource);
                List<T> deserialize = JsonSerializer.Deserialize<List<T>>(asJson);
                return deserialize;
            }
        }
    }
}