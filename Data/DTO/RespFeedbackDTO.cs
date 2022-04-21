using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class RespFeedbackDTO
    {
        public int IdFeedback { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public string CategoryName { get; set; }
        public bool Resolved { get; set; }
    }
}

/**
         public int IdFeedback { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int CategoryIdCategory { get; set; }
        public byte? Resolved { get; set; }
 */