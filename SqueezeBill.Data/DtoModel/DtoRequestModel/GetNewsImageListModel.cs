using EntityDatabase.Models;
using SqueezeBill.Data.DtoModel.DtoResponseModel;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
    public class GetNewsImageListModel :CommonModel
    {
            public int Id { get; set; }
            public string Title { get; set; }
            public string ShortDescription { get; set; }

            public string CompleteDescription { get; set; }

            public string Image { get; set; }

            public List<NewsImage> GetImagebyId { get; set; }

       
    }

    public class Images 
    {
        public int ImageId  { get; set; }
        public string ImagePath { get; set; }
        public int NewsId { get; set; }

    }
}
