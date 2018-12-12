using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class FAQsListResponseModel
    {
        public FAQsListResponseModel()
        {
            Response = new FAQsListDetails();
        }
        public FAQsListDetails Response { get; set; }
    }

    public class FAQsListDetailsModel
    {
        public int? Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }

    public class FAQsListDetails
    {
        public FAQsListDetails()
        {
            Details = new List<FAQsListDetailsModel>();
        }
        public List<FAQsListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
