using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Rebuilding your DB...");

            using (var ctx = new SampleDbContext())
            {
                Console.WriteLine("Your connection string is: {0}", ctx.Database.Connection.ConnectionString);
                Console.WriteLine("Total number of rows in data model: {0}", ctx.Data.Count());
            }
        }
    }
}
