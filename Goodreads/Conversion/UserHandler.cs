using System;
using System.Collections.Generic;
using Goodreads.Models;
using Models.StaticData;

namespace Goodreads.Conversion
{
    public class UserHandler
    {
        private static int numOfUsers = 500;
        private static Random rand = new();

        public static void AddUsers(DataBaseModelContainer container)
        {
            List<Profile> users = GenerateUsers();
            container.Users = users;
            container.UsersHaveRead = GenerateUserReadings(container);
        }

        private static List<BookRead> GenerateUserReadings(DataBaseModelContainer container)
        {
            List<BookRead> list = new();
            foreach (Profile user in container.Users)
            {
                int count = 0;
                Dictionary<string, BookRead> data = new();
                int numOfBooksToRead = rand.Next(25, 750);
                for (int i = 0; i < numOfBooksToRead; i++)
                {
                    BookRead br = GenerateOnBookRead(container, user);
                    if (!data.ContainsKey(br.Book.BookId))
                    {
                        data.Add(br.Book.BookId, br);
                        count++;
                    }
                }
                list.AddRange(data.Values);

                Console.WriteLine($"Added books for user {user.Id}, they have read {count} books");
            }
            return list;
        }

        private static BookRead GenerateOnBookRead(DataBaseModelContainer container, Profile profile)
        {
            BookRead br = new();
            Book bookToRead = null;
            string status = rand.Next(2) == 0 ? "to-read" : "read";
            br.Status = status;
            while (bookToRead == null)
            {
                bookToRead = container.Books[rand.Next(container.Books.Count)];
                if (!bookToRead.YearPublished.HasValue) bookToRead = null;
            }

            br.Book = bookToRead;
            br.Profile = profile;

            if ("to-read".Equals(status))
            {
                br.Rating = null;
                br.DateFinishedReading = null;
                br.DateStartedReading = null;
                return br;
            }
            
            br.Rating = rand.Next(1, 6);
            int yearPublished = (int)br.Book.YearPublished;


            DateTime start = new DateTime(rand.Next(yearPublished, 2022), 1, 1).AddMonths(rand.Next(0, 13)).AddDays(rand.Next(0, 31));
            DateTime end = start.AddDays(rand.Next(4, 50));
            string startReading = start.ToString("yyyy/MM/dd");
            string endReading = end.ToString("yyyy/MM/dd");
            br.DateStartedReading = startReading;
            br.DateFinishedReading = endReading;
            
            return br;
        }


        private static List<Profile> GenerateUsers()
        {
            List<Profile> users = new();
            for (int i = 0; i < UserName.list.Length; i++)
            {
                Profile profile = new()
                {
                    LastName = LastName.list[rand.Next(LastName.list.Length)],
                    FirstName = rand.Next(2) == 0
                        ? FemaleName.list[rand.Next(FemaleName.list.Length)]
                        : MaleName.list[rand.Next(MaleName.list.Length)],
                    Id = (i + 1),
                    ProfileName = UserName.list[i]
                };
                users.Add(profile);
            }

            return users;
        }
    }
}