using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Feedback
    {
        public int IdFeedback { get; set; }
        public DateTime Date { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public int CategoryIdCategory { get; set; }

        public virtual Category CategoryIdCategoryNavigation { get; set; }
    }
}
