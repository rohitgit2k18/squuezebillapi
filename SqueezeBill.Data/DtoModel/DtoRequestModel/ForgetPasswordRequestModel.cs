using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoRequestModel
{
   public class ForgetPasswordRequestModel
    {
        public string EmailId { get; set; }
        public string MobileNum { get; set; }
        public Int32 CountryCode { get; set; }
    }
}
