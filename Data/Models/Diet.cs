using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Diet
    {
        public Diet()
        {
            AnimalHasDiets = new HashSet<AnimalHasDiet>();
        }

        public int IdDiet { get; set; }
        public string Diet1 { get; set; }

        public virtual ICollection<AnimalHasDiet> AnimalHasDiets { get; set; }
    }
}
