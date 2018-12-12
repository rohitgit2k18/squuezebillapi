using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
     public class GetAreaServiceResponseModel
    {
        public GetAreaServiceResponseModel()
        {
            Response = new AreaServiceListDetails();
        }
        public AreaServiceListDetails Response { get; set; }
    }

    public class AreaServiceListDetailsModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }

    public class AreaServiceListDetails
    {
        public AreaServiceListDetails()
        {
            Details = new List<AreaServiceListDetailsModel>();
        }
        public List<AreaServiceListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
