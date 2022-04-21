using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class CreateFeedbackDTO
    {
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int CategoryId { get; set; }
    }
}
