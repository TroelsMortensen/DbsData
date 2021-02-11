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
            List<GoodreadsItem> items = new CSVImporter().ImportItems(@"C:\Users\trmo\OneDrive - ViaUC\Courses\DBS\Session 6 - DQL - Data Query Language (SQL)\goodreads_library_export.csv");

            DataBaseModelContainer container = new Converter().Convert(items);
            new GenreImporter().AddGenres(container.Books);
            new SQLExporter().Export(container);

            // GenreImporter genreImporter = new GenreImporter();
            // genreImporter.FetchOneByOne();
        }
    }
}