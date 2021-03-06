using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string CategoryName { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
