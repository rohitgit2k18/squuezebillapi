using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class WhySwitchingImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public int? WhySwitchingId { get; set; }
    }
}
