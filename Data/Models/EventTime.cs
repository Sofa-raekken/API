using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class EventTime
    {
        public int IdEventTime { get; set; }
        public DateTime Date { get; set; }
        public int FkIdEvent { get; set; }

        public virtual Event FkIdEventNavigation { get; set; }
    }
}
