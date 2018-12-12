using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class HowSwitchingImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public int? HowSwitchingId { get; set; }
    }
}
