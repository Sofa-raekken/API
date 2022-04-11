using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class CreateEventDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public int IdAnimal { get; set; }
    }
}
