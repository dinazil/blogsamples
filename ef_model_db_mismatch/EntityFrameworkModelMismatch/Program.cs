using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkModelMismatch
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new SampleDbContext())
            {
                Console.WriteLine("Total number of rows in data model: {0}", ctx.Data.Count());

                var query = from d in ctx.Data
                            where d.Id == new Guid("2cc3c76e-854d-4c6e-b7ad-23cdc222de1b") && d.Timestamp < new DateTime(2014, 11, 28, 17, 0, 0)
                            select d;

                foreach (var d in query.ToList())
                {
                    Console.WriteLine(d);
                }

            }
        }
    }
}
