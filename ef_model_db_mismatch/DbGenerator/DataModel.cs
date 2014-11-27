using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbGenerator
{
    class DataModel
    {
        [Key, Column(Order = 0)]
        public Guid Id { get; set; }
        [Key, Column(Order = 1)]
        public DateTime Timestamp { get; set; }
        public int Value { get; set; }
    }
}
