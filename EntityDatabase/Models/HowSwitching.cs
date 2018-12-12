using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class HowSwitching
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public bool? IsDelete { get; set; }
    }
}
