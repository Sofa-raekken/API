using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class AnimalHasDiet
    {
        public int AnimalIdAnimal { get; set; }
        public int DietIdDiet { get; set; }

        public virtual Animal AnimalIdAnimalNavigation { get; set; }
        public virtual Diet DietIdDietNavigation { get; set; }
    }
}
