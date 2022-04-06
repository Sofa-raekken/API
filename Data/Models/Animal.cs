using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Animal
    {
        public Animal()
        {
            AnimalHasDiets = new HashSet<AnimalHasDiet>();
            AnimalHasEvents = new HashSet<AnimalHasEvent>();
        }

        public int IdAnimal { get; set; }
        public string Name { get; set; }
        public string LatinName { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string LifeExpectancy { get; set; }
        public string Pregnancy { get; set; }
        public string Heigth { get; set; }
        public string BirthWeight { get; set; }
        public string Qr { get; set; }
        public int SpeciesIdSpecies { get; set; }
        public byte? Disabled { get; set; }

        public virtual Species SpeciesIdSpeciesNavigation { get; set; }
        public virtual ICollection<AnimalHasDiet> AnimalHasDiets { get; set; }
        public virtual ICollection<AnimalHasEvent> AnimalHasEvents { get; set; }
    }
}
