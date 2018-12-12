using EntityDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
   public class GlossaryListModel
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<EnergyGlossaryImage> GetImagebyId { get; set; }
    }

    public class GlossaryList
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public int EnergyGlossaryID { get; set; }

    }
}
