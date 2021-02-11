using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Goodreads.Models;

namespace Goodreads.Import
{
    public class CSVImporter
    {
        public List<GoodreadsItem> ImportItems(string path)
        {
            List<GoodreadsItem> items = new();

            string line;

            using StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if(line.StartsWith("Book")) continue;
                GoodreadsItem goodreadsItem = ImportLine(line);
                items.Add(goodreadsItem);
            }

            return items;
        }

        private GoodreadsItem ImportLine(string line)
        {
            var initialSplit = line.Split(',');
            List<string> corrected = new();
            bool opened = false;
            string temp = "";
            for (int i = 0; i < initialSplit.Length-1; i++)
            {
                if (initialSplit[i].StartsWith("\"="))
                {
                    corrected.Add(initialSplit[i]);
                } else if (!opened && initialSplit[i].StartsWith("\""))
                {
                    opened = true;
                    temp = initialSplit[i].Trim('\"');
                } else if (opened && initialSplit[i].EndsWith("\""))
                {
                    temp += ", " + initialSplit[i].Trim('\"');
                    opened = false;
                    corrected.Add(temp);
                } else if (opened)
                {
                    temp += ", " + initialSplit[i];
                }
                else
                {
                    corrected.Add(initialSplit[i]);
                }
            }

            string bookId = corrected[0];
            var title = corrected[1];
            var authorName = corrected[2];
            var isbn = corrected[5].Replace("\"","").Replace("=","");
            var myRating = int.Parse(corrected[7]);
            var avgRating = decimal.Parse(corrected[8].Replace(".", ","));
            var pubName = corrected[9];
            if (pubName.Contains("Faolan"))
            {
                pubName = "Faolan's Pen Publishing Inc.";
            }
            var binding = corrected[10];
            int? pageCount = String.IsNullOrEmpty(corrected[11]) ? null : int.Parse(corrected[11]);
            int? yearPublished = String.IsNullOrEmpty(corrected[12]) ? null : int.Parse(corrected[12]);
            var dateRead = corrected[14];
            var shelfName = corrected[18];
            List<string> coAuthorNames = String.IsNullOrEmpty(corrected[4]) ? new() : corrected[4].Split(',').ToList();
            GoodreadsItem item = new()
            {
                BookId = bookId,
                Title = title,
                AuthorName = authorName,
                ISBN = isbn,
                MyRating = myRating,
                AvgRating = avgRating,
                PubName = pubName,
                Binding = binding,
                PageCount =pageCount,
                YearPublished = yearPublished,
                DateRead = dateRead,
                ShelfName = shelfName,
                CoAuthorNames = coAuthorNames
            };
            
            return item;
        }
    }
}