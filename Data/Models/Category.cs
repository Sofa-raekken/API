using System;
using System.Collections.Generic;

#nullable disable

namespace Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int IdCategory { get; set; }
        public string Category1 { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
