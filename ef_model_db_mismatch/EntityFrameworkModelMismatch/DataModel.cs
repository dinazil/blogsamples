using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkModelMismatch
{
    class DataModel
    {
        public DataModel()
        {
            Console.WriteLine("Constructing...");
        }

        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} - {2}", Id, Timestamp, Value);
        }
    }
}
