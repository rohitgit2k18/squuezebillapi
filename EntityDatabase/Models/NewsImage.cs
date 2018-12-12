using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class NewsImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public int? NewsId { get; set; }
    }
}
