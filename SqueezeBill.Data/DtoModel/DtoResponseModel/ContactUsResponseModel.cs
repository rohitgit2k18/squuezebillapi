using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class ContactUsResponseModel
    {
        public ContactUsResponseModel()
        {
            Response = new ContactUsDetailModel();
        }
        public ContactUsDetailModel Response { get; set; }
    }

    public class ContactUsDetail
    {       
        public string Content { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }    
        public string PhoneNumber { get; set; }
    }

    public class ContactUsDetailModel
    {
        public ContactUsDetailModel()
        {
            Details = new ContactUsDetail();
        }
        public ContactUsDetail Details { get; set; }
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
