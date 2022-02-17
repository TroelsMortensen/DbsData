using System;
using System.Collections.Generic;
using Goodreads.Conversion;
using Goodreads.Export;
using Goodreads.Import;
using Goodreads.Models;

namespace Goodreads
{
    class Program
    {
        static void Main(string[] args)
        {
            // import data from csv file
            List<GoodreadsItem> items = new CSVImporter().ImportItems(@"C:\TRMO\RiderProjects\DbsData\Goodreads\goodreads_library_export.csv");
            Console.WriteLine("Import from csv done.");
            // convert to C# model classes
            DataBaseModelContainer container = new Converter().ConvertFromCsvModelToDbModel(items);
            Console.WriteLine("Adding users..");
            UserHandler.AddUsers(container);
            Console.WriteLine("All users added");
            // new GenreImporter().StoreBookIds(container.Books); 

            // add genres to the model classes
            Console.WriteLine("Adding genres to books, and container..");
            new GenreImporter().AddGenres(container.Books, container);
            Console.WriteLine("Done");
            // export to sql
            Console.WriteLine("Exporting sql..");
            new SQLExporter().Export(container);
            Console.WriteLine("Done exporting sql");


            // fetches all genres. Run this multiple times, because I can contact goodreads a limited number, before being locked out. Wait, run again. Progress is stored in a file, so as to not start over
            // new GenreImporter().FetchOneByOne();
        }
    }
}