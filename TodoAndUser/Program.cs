using System;
using System.Threading.Tasks;
using TodoAndUser.Data;
using TodoAndUser.Export;
using TodoAndUser.Models;

namespace TodoAndUser
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DataRetriever dataRetriever = new DataRetriever();
            ModelContainer mc = await dataRetriever.RetrieveAll();
            SqlExport exporter = new SqlExport();
            exporter.Export(mc);
            int stopher = 0;
            Console.WriteLine("All done!");
        }
    }
}