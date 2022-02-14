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
            // List<GoodreadsItem> items = new CSVImporter().ImportItems(@"C:\TRMO\RiderProjects\DbsData\Goodreads\goodreads_library_export.csv");
            //
            // DataBaseModelContainer container = new Converter().Convert(items);
            
            // new GenreImporter().StoreBookIds(container.Books); 
            
            // new GenreImporter().AddGenres(container.Books);
            
            // new SQLExporter().Export(container);
            //
            new GenreImporter().FetchOneByOne();
        }
    }
}