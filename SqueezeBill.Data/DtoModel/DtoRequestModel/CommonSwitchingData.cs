using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
   public class CommonSwitchingData
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
       public List<CommenImage> ListImage { get; set; }
    }

    public class CommenImage
    {
        public int ImageId { get; set; }
        public string ImagePaht { get; set; }
        public string WhySwitchingId { get; set; }
    }
    public class FAQ
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
