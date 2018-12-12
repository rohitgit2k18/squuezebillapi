using EntityDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class UserLoginResponseModel
    {
        public string ResponseMessage { get; set; }
        public Int32 StatusCode { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
