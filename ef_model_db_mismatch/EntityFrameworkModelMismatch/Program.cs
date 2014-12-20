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
                            where d.Id == new Guid("82295fa4-e5e0-4152-a5e7-0c06e25f803a") 
                            && d.Timestamp < new DateTime(2014, 12, 2, 19, 0, 0)
                            select d;

                foreach (var d in query)
                {
                    Console.WriteLine(d);
                }

            }
        }
    }
}
