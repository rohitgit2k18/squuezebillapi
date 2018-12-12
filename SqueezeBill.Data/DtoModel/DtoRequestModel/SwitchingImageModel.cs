using EntityDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
   public class SwitchingImageModel
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }



        public List<HowSwitchingImage> GetImagebyId { get; set; }
    }
    public class SwichImage
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public int whSwichId { get; set; }

    }
}
