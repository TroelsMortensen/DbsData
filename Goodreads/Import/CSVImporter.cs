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
                if (!opened && initialSplit[i].StartsWith("\""))
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

            return null;
        }
    }
}