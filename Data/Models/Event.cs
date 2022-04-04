using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Event
    {
        public Event()
        {
            AnimalHasEvents = new HashSet<AnimalHasEvent>();
        }

        public int IdEvent { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<AnimalHasEvent> AnimalHasEvents { get; set; }
    }
}
