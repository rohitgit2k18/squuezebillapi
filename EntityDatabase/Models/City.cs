using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class City
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
