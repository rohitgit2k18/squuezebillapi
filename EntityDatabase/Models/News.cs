using System;
using System.Collections.Generic;

namespace EntityDatabase.Models
{
    public partial class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public int? ImageId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string ShortDescription { get; set; }
        public string CompleteDescription { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
