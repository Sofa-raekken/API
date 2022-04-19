using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RespEventTimestampDTO
    {
        public string EventName { get; set; }
        public DateTime OccurringDate { get; set; }
        public int EventId { get; set; }
    }
}
