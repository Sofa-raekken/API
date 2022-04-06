using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class UpdateAnimalDTO
    {
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
        public bool? Disabled { get; set; }
    }
}
