using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class ServiceAreaImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public int? ServiceAreaId { get; set; }
    }
}
