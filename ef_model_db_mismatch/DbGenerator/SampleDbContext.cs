using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbGenerator
{
    class SampleDbContext : DbContext
    {
        public SampleDbContext()
        {
            Database.SetInitializer(new SampleDbInitializer());
        }
        public DbSet<DataModel> Data { get; set; }
    }
}
