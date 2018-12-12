using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class EnergyGlossaryImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public int? EnergyGlossaryId { get; set; }
    }
}
