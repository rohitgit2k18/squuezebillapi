using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class State
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
