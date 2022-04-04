using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class AnimalHasEvent
    {
        public int AnimalIdAnimal { get; set; }
        public int EventIdEvent { get; set; }

        public virtual Animal AnimalIdAnimalNavigation { get; set; }
        public virtual Event EventIdEventNavigation { get; set; }
    }
}
