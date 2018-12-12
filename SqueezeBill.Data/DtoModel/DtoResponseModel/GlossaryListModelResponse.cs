using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class GlossaryListModelResponse
    {
        public GlossaryListModelResponse()
        {
            Response = new GlossaryListDetails();
        }
        public GlossaryListDetails Response { get; set; }
    }

    public class GlossaryListDetailsModel
    {
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }

    public class GlossaryListDetails
    {
        public GlossaryListDetails()
        {
            Details = new List<GlossaryListDetailsModel>();
        }
        public List<GlossaryListDetailsModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
