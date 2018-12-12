using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class ZipCode
    {
        public int ZipCodeid { get; set; }
        public string Code { get; set; }
        public int? CityId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
