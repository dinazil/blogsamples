using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbGenerator
{
    class SampleDbInitializer : DropCreateDatabaseAlways<SampleDbContext>
    {
        protected override void Seed(SampleDbContext context)
        {
            const int uniqeGuidCount = 10;
            const int totalCount = 1000;
            var guids = Enumerable.Range(0, uniqeGuidCount).Select(_ => Guid.NewGuid()).ToList();
            var random = new Random();
            var baseTime = DateTime.Now;
            for (int i = 0; i < totalCount; ++i)
            {
                context.Data.Add(new DataModel
                {
                    Id = guids[random.Next(uniqeGuidCount)],
                    Timestamp = baseTime + TimeSpan.FromMinutes(i),
                    Value = random.Next()
                });
            }

            base.Seed(context);
        }
    }
}
