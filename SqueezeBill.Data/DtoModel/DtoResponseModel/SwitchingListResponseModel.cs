using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class SwitchingListResponseModel
    {
        public SwitchingListResponseModel()
        {
            Response = new SwitchingListDetails();
        }
        public SwitchingListDetails Response { get; set; }
    }

    public class SwitchingListDetailsModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }

    public class SwitchingListDetails
    {
        public SwitchingListDetails()
        {
            Details = new List<SwitchingListDetailsModel>();
        }
        public List<SwitchingListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
