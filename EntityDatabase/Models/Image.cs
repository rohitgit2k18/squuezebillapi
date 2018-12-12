using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
