using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class WhySwitchListResponseModel
    {
        public WhySwitchListResponseModel()
        {
            Response = new WhySwitchListDetails();
        }
        public WhySwitchListDetails Response { get; set; }
    }

    public class WhySwitchListDetailsModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string ImagePaht { get; set; }
    }

    public class WhySwitchListDetails
    {
        public WhySwitchListDetails()
        {
            Details = new List<WhySwitchListDetailsModel>();
        }
        public List<WhySwitchListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
