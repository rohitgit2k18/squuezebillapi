using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
  

    public class UserRegistrationResponseModel
    {
        public UserRegistrationResponseModel()
        {
            Response = new UserRegistration();
        }
        public UserRegistration Response { get; set; }
    }

    public class UserRegistration
    {
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
