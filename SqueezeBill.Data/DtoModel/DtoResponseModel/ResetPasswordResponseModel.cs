using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
    public class ResetPasswordResponseModel
    {
        public ResetPasswordResponseModel()
        {
            Response = new ResetPassword();
        }
        public ResetPassword Response { get; set; }
    }

    public class ResetPassword
    {
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }

  
}
