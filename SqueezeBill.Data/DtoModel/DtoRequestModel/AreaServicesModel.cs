using EntityDatabase.Models;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
   public class AreaServicesModel
    {
        public int Id { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public List<ServiceAreaImage> GetImagebyId { get; set; }
    }

    public class AreaaimageList
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
        public int EnergyGlossaryId { get; set; }

    }
}

