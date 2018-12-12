using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class Faq
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool? IsDelete { get; set; }
    }
}
