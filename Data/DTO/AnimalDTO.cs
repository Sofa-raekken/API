using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class AnimalDTO
    {
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

        public virtual SpeciesDTO SpeciesIdSpeciesNavigation { get; set; }
        public virtual IEnumerable<DietDTO> Diets { get; set; }
        public virtual IEnumerable<EventDTO> Events { get; set; }
    }
}
