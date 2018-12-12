using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class NewsListResponseModel
    {
        public NewsListResponseModel()
        {
            Response = new NewsListDetails();
        }
        public NewsListDetails Response { get; set; }
    }

    public class NewsListDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string CompleteDescription { get; set; }
        public string Image { get; set; }
    }

    public class NewsListDetails
    {
        public NewsListDetails()
        {
            Details = new List<NewsListDetailsModel>();
        }
        public List<NewsListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
