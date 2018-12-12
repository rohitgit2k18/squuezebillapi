using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? Modifieddate { get; set; }
    }
}
