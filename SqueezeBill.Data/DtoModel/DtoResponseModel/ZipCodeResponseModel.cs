using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class ZipCodeResponseModel
    {
        public ZipCodeResponseModel()
        {
            Response = new ZipCodeDetail();
        }
        public ZipCodeDetail Response { get; set; }
    }

    public class ZipCodeModelDetailModel
    {
        public string ZipCode { get; set; }
    }

    public class ZipCodeDetail
    {
        public ZipCodeDetail()
        {
            Details = new List<ZipCodeModelDetailModel>();
        }
        public List<ZipCodeModelDetailModel> Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
