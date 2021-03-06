using Data.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class CreateAnimalDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string LatinName { get; set; }

        [Required]
        public string Description { get; set; }
        public string Weight { get; set; }
        public string LifeExpectancy { get; set; }
        public string Pregnancy { get; set; }
        public string Heigth { get; set; }
        public string BirthWeight { get; set; }
        public string Qr { get; set; }

        [Required]
        [JsonProperty("species")]
        public int SpeciesIdSpecies { get; set; }

        public List<DietDTO> Diets { get; set; }
    }
}
