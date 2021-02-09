using System;
using System.Collections.Generic;
using Goodreads.Import;
using Goodreads.Models;

namespace Goodreads
{
    class Program
    {
        static void Main(string[] args)
        {
            List<GoodreadsItem> items = new CSVImporter().ImportItems(
                @"C:\Users\trmo\OneDrive - ViaUC\Courses\DBS\Session 6 - DQL - Data Query Language (SQL)\goodreads_library_export.csv");
        }
    }
}